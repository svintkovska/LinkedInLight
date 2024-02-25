using AutoMapper;
using DLL.Models;
using DLL.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
			var userDTO = new UserDTO { UserName = email, Email = email, EmailConfirmed = true };
			ApplicationUser user = _mapper.Map<ApplicationUser>(userDTO);

			var result = await _userManager.CreateAsync(user, password);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					// Check if the error is related to password requirements
					if (error.Code == "PasswordRequiresNonAlphanumeric" ||
						error.Code == "PasswordRequiresLower" ||
						error.Code == "PasswordRequiresUpper")
					{
						// You can choose to throw an exception, return a specific result,
						// or handle the error in a different way
						throw new Exception("Password requirements not met");
					}
				}
			}
			return result.Succeeded;
		}

		public async Task<bool> Login(string email, string password)
		{
			var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
			return result.Succeeded;
		}
	}

}
