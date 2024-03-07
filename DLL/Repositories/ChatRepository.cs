using DLL.Data;
using DLL.Repositories.IRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
	public class ChatRepository : Repository<Chat>, IChat
	{
		private readonly ApplicationDbContext _db;
		public ChatRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public async Task<List<Chat>> GetChatsForUser(string userId)
		{
			return await _db.Chats
				.Where(c => (c.Participant1Id == userId || c.Participant2Id == userId) &&
							(!c.Messages.Any(m => m.IsDeletedForSender && m.SenderId == userId) &&
							 !c.Messages.Any(m => m.IsDeletedForReceiver && m.ReceiverId == userId)))
				.Include(c => c.Messages)
				.ToListAsync();
		}

	}
}
