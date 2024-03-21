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
	public class OpenToWorkCityRepository : Repository<OpenToWorkCity>, IOpenToWorkCity
	{
		private readonly ApplicationDbContext _db;
		public OpenToWorkCityRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}
