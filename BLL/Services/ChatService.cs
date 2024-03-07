using BLL.Interfaces;
using BLL.Utilities.SignalR;
using DLL.Repositories;
using DLL.Repositories.IRepository;
using Domain.Models;
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
		public async Task SendMessage(string userId, int chatId, Message message)
		{
		    message = new Message
			{
				Content = message.Content,
				SentAt = DateTime.UtcNow,
				IsRead = false,
				SenderId = message.SenderId,
				ReceiverId = message.ReceiverId
			};

			await _unitOfWork.MessageRepo.Add(message);
			await _unitOfWork.SaveAsync();


			///
			var chats = await GetChatsForUser(userId);
			var messages = await GetMessagesFromChat(chatId, userId);
			await _hubContext.Clients.All.SendAsync("UpdateChatList", chats);
			await _hubContext.Clients.All.SendAsync("UpdateMessages", messages);
			await _hubContext.Clients.All.SendAsync("ReceiveMessage", userId, message);
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

		public async Task DeleteMessageForMe(string userId, int messageId, int chatId)
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
			}
		}

		public async Task DeleteChatForAll(int chatId, string userId)
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
			}
		}
		public async Task DeleteChatForMe(int chatId, string userId)
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
			}
		}

		

	}
}
