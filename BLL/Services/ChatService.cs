using BLL.Interfaces;
using BLL.Utilities.SignalR;
using BLL.ViewModels;
using DLL.Repositories;
using DLL.Repositories.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class ChatService : IChatService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHubContext<ChatHub> _hubContext;

		public ChatService(IUnitOfWork unitOfWork, IHubContext<ChatHub> hubContext)
		{
			_unitOfWork = unitOfWork;
			_hubContext = hubContext;
		}
		public async Task<List<Chat>> GetChatsForUser(string userId)
		{
			return await _unitOfWork.ChatRepo.GetChatsForUser(userId);
		}
		public async Task<List<Message>> GetMessagesFromChat(int chatId, string userId)
		{
			return await _unitOfWork.MessageRepo.GetMessagesFromChat(chatId, userId);
		}
		public async Task SendMessage( MessageVM message)
		{

			var existingChat = await _unitOfWork.ChatRepo.Get(c =>
				(c.Participant1Id == message.SenderId && c.Participant2Id == message.ReceiverId) ||
				(c.Participant1Id == message.ReceiverId && c.Participant2Id == message.SenderId));

			if (existingChat == null)
			{
				Chat newChat = new Chat
				{
					Participant1Id = message.SenderId,
					Participant2Id = message.ReceiverId,
					IsRead = false,
					IsDeletedForParticipant1 = false,
					IsDeletedForParticipant2 = false,
					Messages = new List<Message> { }
				};

				await _unitOfWork.ChatRepo.Add(newChat);
				await _unitOfWork.SaveAsync();

				existingChat = newChat; 
			}



			Message msg = new Message
			{
				Content = message.Content,
				SentAt = DateTime.UtcNow,
				IsRead = false,
				IsDeletedForSender= false,
				IsDeletedForReceiver= false,
				SenderId = message.SenderId,
				ReceiverId = message.ReceiverId,
				ChatId = existingChat.Id
			};

			await _unitOfWork.MessageRepo.Add(msg);
			await _unitOfWork.SaveAsync();

			existingChat.LastMessageId = msg.Id;
			existingChat.LastMessageSentAt = msg.SentAt;
			await _unitOfWork.SaveAsync();

			///
			var chats = await GetChatsForUser(message.SenderId);
			var messages = await GetMessagesFromChat(existingChat.Id, message.SenderId);
			await _hubContext.Clients.All.SendAsync("UpdateChatList", chats);
			await _hubContext.Clients.All.SendAsync("UpdateMessages", messages);
			await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.SenderId, msg);
		}

		public async Task MarkMessagesAsRead(int chatId, string userId)
		{
			var messages = await _unitOfWork.MessageRepo.GetMessagesFromChat(chatId, userId);
			foreach (var message in messages)
			{
				if (message.IsRead == false && message.ReceiverId == userId)
				{
					message.IsRead = true;
					_unitOfWork.MessageRepo.Update(message);
				}
			}
			await _unitOfWork.SaveAsync();

			////

			var updtMessages = await GetMessagesFromChat(chatId, userId);
			await _hubContext.Clients.All.SendAsync("MessagesMarkedAsRead", updtMessages);
		}
		public async Task UpdateMessage(Message message)
		{
			var existingMessage = await _unitOfWork.MessageRepo.Get(m=>m.Id == message.Id);
			if (existingMessage == null)
			{
				throw new Exception("Message not found");
			}

			existingMessage.Content = message.Content;

			 _unitOfWork.MessageRepo.Update(existingMessage);
			await _unitOfWork.SaveAsync();

			///
			await _hubContext.Clients.All.SendAsync("MessageUpdated", message);

		}

		public async Task<bool> DeleteMyMessageForAll(string userId, int messageId, int chatId)
		{
			var message = await _unitOfWork.MessageRepo.Get(m => m.Id == messageId && m.SenderId == userId);
			if (message != null)
			{
				_unitOfWork.MessageRepo.Remove(message);
				await _unitOfWork.SaveAsync();


				///
				var messages = await GetMessagesFromChat(chatId, userId);
				await _hubContext.Clients.All.SendAsync("MessageDeletedForAll", messages);
				return true;
			}
			return false;
		}

		public async Task<bool> DeleteMessageForMe(string userId, int messageId, int chatId)
		{
			var message = await _unitOfWork.MessageRepo.Get(m => m.Id == messageId);
			if (message != null)
			{
				if (message.SenderId == userId)
				{
					message.IsDeletedForSender = true;
				}

				if (message.ReceiverId == userId)
				{
					message.IsDeletedForReceiver = true;
				}

				 _unitOfWork.MessageRepo.Update(message);
				await _unitOfWork.SaveAsync();
				
				///
				var messages = await GetMessagesFromChat(chatId, userId);
				await _hubContext.Clients.All.SendAsync("MessageDeletedForMe", messages);

				return true;
			}
			return false;

		}

		public async Task<bool> DeleteChatForAll(int chatId, string userId)
		{
			var chat = await _unitOfWork.ChatRepo.Get(c=>c.Id == chatId);
			if (chat != null)
			{
				_unitOfWork.MessageRepo.RemoveRange(chat.Messages);
				_unitOfWork.ChatRepo.Remove(chat);
				await _unitOfWork.SaveAsync();

				///
				var chats = await GetChatsForUser(userId);
				await _hubContext.Clients.All.SendAsync("ChatDeletedForAll", chats);

				return true;
			}
			return false;
		}
		public async Task<bool> DeleteChatForMe(int chatId, string userId)
		{
			var chat = await _unitOfWork.ChatRepo.Get(c=>c.Id == chatId);
			if (chat != null)
			{
				if (chat.Participant1Id == userId)
				{
					chat.IsDeletedForParticipant1 = true;
				}
				else if (chat.Participant2Id == userId)
				{
					chat.IsDeletedForParticipant2 = true;
				}

				foreach (var message in chat.Messages)
				{
					if (message.SenderId == userId)
					{
						message.IsDeletedForSender = true;
					}
					else if (message.ReceiverId == userId)
					{
						message.IsDeletedForReceiver = true;
					}
				}

				await _unitOfWork.SaveAsync();

				///
				var chats = await GetChatsForUser(userId);
				await _hubContext.Clients.All.SendAsync("ChatDeletedForMe", chats);

				return true;
			}
			return false;
		}

		

	}
}
