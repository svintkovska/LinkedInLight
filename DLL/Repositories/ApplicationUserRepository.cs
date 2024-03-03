using DLL.Data;
using Domain.Models;
using DLL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
	public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
	{
		private readonly ApplicationDbContext _db;
		public ApplicationUserRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task<ApplicationUser> GetByEmail(string email)
		{
			return await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
		}
	}
}
