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
	public class ProjectRepository: Repository<Project>, IProject
	{
		private readonly ApplicationDbContext _db;
		public ProjectRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task<List<Project>> GetUserProjects(string userId)
		{
			return await _db.Projects
				.Include(p => p.ProjectContributors)
				.Where(u => u.ApplicationUserId == userId)
				.ToListAsync();
		}
		public async Task<Project> GetProjectWithContributors(int projectId)
		{
			return await _db.Projects
				.Include(p => p.ProjectContributors)
				.FirstOrDefaultAsync(p => p.Id == projectId);
		}

	}
}
