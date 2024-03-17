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
	public class ExperienceRepository : Repository<Experience>, IExperience
	{
		private readonly ApplicationDbContext _db;
		public ExperienceRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task<List<Experience>> GetUserExperiencesWithIndustry(string userId)
		{
			return await _db.ApplicationUsers
				.Where(u => u.Id == userId)
				.SelectMany(u => u.Experiences)
				.Include(e => e.Industry)
				.ToListAsync();
		}
	}
}
