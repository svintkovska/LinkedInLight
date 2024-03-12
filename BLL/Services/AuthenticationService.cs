using BLL.Interfaces;
using DLL.Repositories.IRepository;
using DLL.Constants;
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
using Domain.Models;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace BLL.Services
{
    public class AuthenticationService: IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IJwtTokenService _jwtTokenService;
		private readonly IConfiguration _configuration;
		private readonly ISendGridService _sendGridService;

		public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
			IJwtTokenService jwtTokenService, IConfiguration configuration, ISendGridService sendGridService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtTokenService = jwtTokenService;
			_configuration = configuration;
			_sendGridService = sendGridService;
		}
		public async Task<bool> IsValidEmail(string email)
		{
			var existingUser = await _userManager.FindByEmailAsync(email);
			if (existingUser != null)
			{
				throw new Exception("Email already exists");
			}
			return true;
		}

		public async Task<string> SendConfirmationCode(string email)
		{
			var code = GenerateRandom6DigitCode();
			string subject = "Confirm Your Email";
			string htmlMessage = $"<p>Please type in the following code to confirm your email: {code}</p>";

			 await _sendGridService.SendEmailAsync(email, subject, htmlMessage);

			return code;
		}
		public async Task<bool> Register(RegisterVM model)
		{
			var user = new ApplicationUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true, FirstName = model.FirstName, LastName = model.LastName };

			var result = await _userManager.CreateAsync(user, model.Password);
			
			if (!result.Succeeded)
			{
				throw new Exception("Error when creating a user");
			}
			else
			{
				result = _userManager.AddToRoleAsync(user, RoleConstants.AUTHORIZED_USER).Result;
				await _userManager.UpdateAsync(user);

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
				User = user,
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

					await _userManager.AddToRoleAsync(user, RoleConstants.AUTHORIZED_USER);
					var token = await _jwtTokenService.CreateToken(user);

					var roles = await _userManager.GetRolesAsync(user);

					var loginResult = new LoginResultVM
					{
						Success = true,
						User = user,
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

					await _userManager.AddToRoleAsync(user, RoleConstants.AUTHORIZED_USER);
					token = await _jwtTokenService.CreateToken(user);
					var roles = await _userManager.GetRolesAsync(user);

					var loginResult = new LoginResultVM
					{
						Success = true,
						User = user,
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
				User = user,
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

			await _sendGridService.SendEmailAsync(user.Email, "Reset Password", htmlContent);


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
		private string GenerateRandom6DigitCode()
		{
			Random random = new Random();
			int code = random.Next(100000, 999999);
			return code.ToString();
		}
	}

}
