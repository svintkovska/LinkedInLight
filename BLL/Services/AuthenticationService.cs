using AutoMapper;
using DLL.Models;
using DLL.Repositories.IRepository;
using DLL.Utilities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class AuthenticationService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IMapper _mapper;

		public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
		}

		public async Task<bool> Register(string email, string password)
		{
			var existingUser = await _userManager.FindByEmailAsync(email);
			if (existingUser != null)
			{
				throw new Exception("Email already exists");
			}


			var userDTO = new UserDTO { UserName = email, Email = email, EmailConfirmed = true };
			ApplicationUser user = _mapper.Map<ApplicationUser>(userDTO);

			var result = await _userManager.CreateAsync(user, password);
			if (!result.Succeeded)
			{
				throw new Exception("Password requirements not met (min 6 characters inclusing lower, upper, digit and non alphanumeric)");
			}
			else
			{
				result = _userManager.AddToRoleAsync(user, RoleConstants.USER).Result;
			}
			return result.Succeeded;
		}

		public async Task<bool> Login(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				throw new Exception("User not found");
			}

			var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
			if (!result.Succeeded)
			{
				throw new Exception("Invalid password");
			}

			return result.Succeeded;
		}
	}

}
