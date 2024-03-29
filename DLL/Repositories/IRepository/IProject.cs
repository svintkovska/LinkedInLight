﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories.IRepository
{
	public interface IProject: IRepository<Project>
	{
		public Task<List<Project>> GetUserProjects(string userId);
		public Task<Project> GetProjectWithContributors(int projectId);
	}
}
