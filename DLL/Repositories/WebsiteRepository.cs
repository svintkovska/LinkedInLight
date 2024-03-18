using DLL.Data;
using DLL.Repositories.IRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
	internal class WebsiteRepository: Repository<Website>, IWebsite
	{
		private readonly ApplicationDbContext _db;
		public WebsiteRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}
