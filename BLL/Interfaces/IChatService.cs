using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IChatService
	{
		public Task SendMessage(Message message);
		public Task MarkMessagesAsRead(string senderId, string receiverId);

	}
}
