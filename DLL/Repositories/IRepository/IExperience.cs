﻿using DLL.Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories.IRepository
{
	public interface IExperience : IRepository<Experience>
	{
		public Task<List<Experience>> GetUserExperiencesWithIndustryAndCompany(string userId);
		public string GetUserLastPosition(string userId);

	}
}
