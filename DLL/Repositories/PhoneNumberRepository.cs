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
	public class PhoneNumberRepository: Repository<PhoneNumber>, IPhoneNumber
	{
		private readonly ApplicationDbContext _db;
		public PhoneNumberRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}
