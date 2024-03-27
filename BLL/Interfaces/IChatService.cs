using BLL.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IChatService
	{
		public  Task<List<Chat>> GetChatsForUser(string userId);
		public Task<List<MessageVM>> GetMessagesFromChat(int chatId, string userId);
		public Task SendMessage(MessageVM message, IFormFile? attachment);
		public Task UpdateMessage(Message message);
		public  Task<bool> DeleteMyMessageForAll(string userId, int messageId, int chatId);
		public Task<bool> DeleteMessageForMe(string userId, int messageId, int chatId);
		public Task<bool> DeleteChatForAll(int chatId, string userId);
		public Task<bool> DeleteChatForMe(int chatId, string userId);
		public Task MarkMessagesAsRead(int chatId, string userId);
	}
}
