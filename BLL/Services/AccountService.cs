using BLL.Interfaces;
using BLL.ViewModels;
using DLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class AccountService: IAccountService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<bool> ChangePassword( ChangePasswordModel model)
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
