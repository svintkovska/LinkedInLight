using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories.IRepository
{
	public interface IApplicationUserRepository : IRepository<ApplicationUser>
	{
		public Task<ApplicationUser> GetByEmail(string email);

	}
}
