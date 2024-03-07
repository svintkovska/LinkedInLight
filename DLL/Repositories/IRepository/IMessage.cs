using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories.IRepository
{
	public interface IMessage: IRepository<Message>
	{
		public Task DeleteAllMessagesFromChat(string senderId, string receiverId);
		public Task<List<Message>> GetMessagesFromChat(int chatId, string userId);

	}
}
