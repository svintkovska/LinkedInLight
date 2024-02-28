using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
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
using BLL.ViewModels.AuthModels;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using BLL.ViewModels;

namespace BLL.Services
{
    public class AuthenticationService: IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IMapper _mapper;
		private readonly IJwtTokenService _jwtTokenService;
		private readonly IConfiguration _configuration;
		private readonly ISmtpEmailService _emailService;

		public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
			IMapper mapper, IJwtTokenService jwtTokenService, IConfiguration configuration, ISmtpEmailService emailService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
			_jwtTokenService = jwtTokenService;
			_configuration = configuration;
			_emailService = emailService;
		}

		public async Task<bool> Register(RegisterVM model)
		{
			var existingUser = await _userManager.FindByEmailAsync(model.Email);
			if (existingUser != null)
			{
				throw new Exception("Email already exists");
			}


			var userDTO = new UserDTO { UserName = model.Email, Email = model.Email, EmailConfirmed = true, FirstName = model.FirstName, LastName = model.LastName };
			ApplicationUser user = _mapper.Map<ApplicationUser>(userDTO);

			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
			{
				throw new Exception("Password requirements not met (min 6 characters including a digit)");
			}
			else
			{
				result = _userManager.AddToRoleAsync(user, RoleConstants.USER).Result;
			}
			return result.Succeeded;
		}

		public async Task<LoginResultVM> Login(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return new LoginResultVM { Success = false };
			}

			var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
			if (!result.Succeeded)
			{
				throw new Exception("Invalid password");
			}


			var token = await _jwtTokenService.CreateToken(user);
			var roles = await _userManager.GetRolesAsync(user);

			return new LoginResultVM
			{
				Success = true,
				User = _mapper.Map<UserDTO>(user),
				Roles = roles,
				Token = token
			};
		}

		public async Task<LoginResultVM> GoogleRegistration(GoogleVM registrationModel)
		{
			var payload = await _jwtTokenService.VerifyGoogleToken(registrationModel.Token);
			if (payload == null)
			{
				throw new Exception("Invalid token");
			}

			string provider = "Google";
			var info = new UserLoginInfo(provider, payload.Subject, provider);
			var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
			if (user == null)
			{
				user = await _userManager.FindByEmailAsync(payload.Email);
				if (user == null)
				{
					user = new ApplicationUser
					{
						Email = payload.Email,
						UserName = registrationModel.UserName,
						FirstName = registrationModel.FirstName,
						LastName = registrationModel.LastName,

					};
					var resultCreate = await _userManager.CreateAsync(user);
					if (!resultCreate.Succeeded)
					{
						throw new Exception("Google registartion failed");
					}

					await _userManager.AddToRoleAsync(user, RoleConstants.USER);
					var token = await _jwtTokenService.CreateToken(user);

					var roles = await _userManager.GetRolesAsync(user);

					var loginResult = new LoginResultVM
					{
						Success = true,
						User = _mapper.Map<UserDTO>(user),
						Token = token,
						Roles = roles
					};
					return (loginResult);

				}
			}

			throw new Exception("Google registartion failed");
		}

		public async Task<LoginResultVM> GoogleLogin(GoogleVM model)
		{
			var payload = await _jwtTokenService.VerifyGoogleToken(model.Token);
			var token = "";
			if (payload == null)
			{
				throw new Exception("Google Log In failed");

			}
			string provider = "Google";
			var info = new UserLoginInfo(provider, payload.Subject, provider);
			var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
			if (user == null)
			{
				user = await _userManager.FindByEmailAsync(payload.Email);
				if (user == null)
				{
					user = new ApplicationUser
					{
						Email = payload.Email,
						UserName = payload.Email,
						FirstName = model.FirstName,
						LastName = model.LastName,

					};
					var resultCreate = await _userManager.CreateAsync(user);
					if (!resultCreate.Succeeded)
					{
						throw new Exception("Google registartion failed");
					}

					await _userManager.AddToRoleAsync(user, RoleConstants.USER);
					token = await _jwtTokenService.CreateToken(user);
					var roles = await _userManager.GetRolesAsync(user);

					var loginResult = new LoginResultVM
					{
						Success = true,
						User = _mapper.Map<UserDTO>(user),
						Token = token,
						Roles = roles
					};
					return (loginResult);

				}


				var resultuserLogin = await _userManager.AddLoginAsync(user, info);
				if (!resultuserLogin.Succeeded)
				{
					throw new Exception("Google Log In failed");
				}
			}
			
			token = await _jwtTokenService.CreateToken(user);

			var userRoles = await _userManager.GetRolesAsync(user);

			var loginResult2 = new LoginResultVM
			{
				Success = true,
				User = _mapper.Map<UserDTO>(user),
				Token = token,
				Roles = userRoles
			};
			return (loginResult2);

		}

		public async Task<bool> ForgotPassword(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				throw new Exception("Email not found");

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var frontendURL = _configuration.GetValue<string>("FrontEndURL");
			var callbackURL = $"{frontendURL}/resetpassword?userId={user.Id}&" + $"code={WebUtility.UrlEncode(token)}";

			string basePath = AppContext.BaseDirectory;
			string htmlFilePath = Path.Combine(basePath, "html", "messageHtml.html");
			string htmlContent = System.IO.File.ReadAllText(htmlFilePath);

			htmlContent = htmlContent.Replace("{{callbackURL}}", callbackURL);

			var message = new SmtpMessage()
			{
				To = user.Email,
				Subject = "Reset Password",
				Body = htmlContent,
			};

			_emailService.Send(message);

			return true;
		}

		public async Task<bool> SetNewPassword(NewPasswordVM model)
		{
			var user = await _userManager.FindByIdAsync(model.UserId);
			var res = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
			if (!res.Succeeded)
			{
				throw new Exception("Setting new password failed");

			}
			return true;
		}
    }

}
