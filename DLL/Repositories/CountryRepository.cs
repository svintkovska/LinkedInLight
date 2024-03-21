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
	public class CountryRepository: Repository<Country>, ICountry
	{
		private readonly ApplicationDbContext _db;
		public CountryRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}
