using DLL.Data;
using DLL.Models;
using DLL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
	public class ConnectionRepository : Repository<Connection>, IConnection
	{
		private readonly ApplicationDbContext _db;
		public ConnectionRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}


	}
}
