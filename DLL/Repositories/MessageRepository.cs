using DLL.Data;
using Domain.Models;
using DLL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
	public class MessageRepository : Repository<Message>, IMessage
	{
		private readonly ApplicationDbContext _db;
		public MessageRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}


	}
}
