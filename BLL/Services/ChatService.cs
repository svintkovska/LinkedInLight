using BLL.Interfaces;
using DLL.Repositories;
using DLL.Repositories.IRepository;
using Domain.Models;
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
		
		public ChatService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task SendMessage(Message message)
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
			await _unitOfWork.MessageRepo.Save();
		}

		public async Task MarkMessagesAsRead(string senderId, string receiverId)
		{
			var messages = await _unitOfWork.MessageRepo.GetUnreadMessagesBySenderReceiver(senderId, receiverId);
			foreach (var message in messages)
			{
				message.IsRead = true;
				_unitOfWork.MessageRepo.Update(message);
			}
			await _unitOfWork.MessageRepo.Save();
		}
	}
}
