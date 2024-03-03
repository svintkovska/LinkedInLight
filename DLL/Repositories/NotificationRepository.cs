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
	public class NotificationRepository : Repository<Notification>, INotification
	{
		private readonly ApplicationDbContext _db;
		public NotificationRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}


	}
}
