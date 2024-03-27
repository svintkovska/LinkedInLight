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
	public class ServiceCityRepository : Repository<ServiceCity>, IServiceCity
	{
		private readonly ApplicationDbContext _db;
		public ServiceCityRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}
