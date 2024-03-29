﻿using DLL.Data;
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

		public async Task<ApplicationUser> GetUserProfileProps(string id)
		{
			return await _db.ApplicationUsers
					.Include(u => u.UserSkills)
						.ThenInclude(s=>s.Skill)
					.Include(u => u.UserLanguages)
						.ThenInclude(l=>l.Language)
					.Include(u=> u.Certifications)
					.Include(u=> u.Projects)
						.ThenInclude(e=> e.ProjectContributors)
					.Include(u=>u.PhoneNumbers)
					.Include(u=>u.Websites)
					.Include(u=>u.Courses)
					.Include(u=>u.ReceivedRecommendations)
					.Include(u=>u.GivenRecommendations)
					.Include(u=> u.VolunteerExperiences)
					.Include(u => u.Posts)
					.Include(u => u.Educations)
					.Include(u => u.Experiences)
						.ThenInclude(e => e.Industry)
					.FirstOrDefaultAsync(u => u.Id == id);
		}
	}
}
