using DLL.Data;
using Domain.Models;
using DLL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
	public class MessageRepository : Repository<Message>, IMessage
	{
		private readonly ApplicationDbContext _db;
		public MessageRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public async Task DeleteAllMessagesFromChat(string senderId, string receiverId)
		{
			var messages = await _db.Messages
			   .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
						(m.SenderId == receiverId && m.ReceiverId == senderId))
			   .ToListAsync();

			_db.Messages.RemoveRange(messages);
			await _db.SaveChangesAsync();
		}
		public async Task<List<Message>> GetMessagesFromChat(int chatId, string userId)
		{
			return await _db.Messages
				.Where(m => m.ChatId == chatId &&
							(!m.IsDeletedForSender || m.SenderId != userId) &&
							(!m.IsDeletedForReceiver || m.ReceiverId != userId))
				.ToListAsync();
		}
	}
}
