using BLL.Interfaces;
using BLL.ViewModels.AuthModels;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class ProfileSecurityService: IProfileSecurityService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public ProfileSecurityService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task<bool> ChangePassword(ChangePasswordVM model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				throw new Exception("User not found");
			}
			var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
			if (!result.Succeeded)
			{
				throw new Exception("Error when changing password");
			}
			return result.Succeeded;

		}
	}
}
