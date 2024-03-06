using BLL.Interfaces;
using BLL.Services;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utilities.SignalR
{
	public class ChatHub: Hub
	{
		private readonly IChatService _chatService;

		public ChatHub(IChatService chatService)
		{
			_chatService = chatService;
		}
		public async Task SendMessage(string user, Message message)
		{
			await _chatService.SendMessage(message);
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}

		private async Task MarkMessagesAsRead(string senderId, string receiverId)
		{
			await _chatService.MarkMessagesAsRead(senderId, receiverId);
		}
	}
}
