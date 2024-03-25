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

		public async Task<List<Experience>> GetUserExperiencesWithIndustryAndCompany(string userId)
		{
			return await _db.ApplicationUsers
				.Where(u => u.Id == userId)
				.SelectMany(u => u.Experiences)
				.Include(e => e.Industry)
				.Include(e => e.Company)
				.ToListAsync();
		}

		public string GetUserLastPosition(string userId)
		{
			var lastPosition = _db.Experiences.Where(e => e.ApplicationUserId == userId)
											  .OrderByDescending(e => e.EndDate.HasValue ? e.EndDate : DateTime.MaxValue)
											  .ThenByDescending(e => e.StartDate)
											  .FirstOrDefault();

			return lastPosition?.Title ?? " ";
		}
	}
}
