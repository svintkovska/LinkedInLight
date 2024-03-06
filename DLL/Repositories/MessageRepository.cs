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

		public async Task<List<Message>> GetUnreadMessagesBySenderReceiver(string senderId, string receiverId)
		{
			var messages = await _db.Messages
			.Where(m => m.ReceiverId == receiverId && m.SenderId == senderId && !m.IsRead)
			.OrderBy(m => m.SentAt)
			.ToListAsync();

			return messages;
		}
	}
}
