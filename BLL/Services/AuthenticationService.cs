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
using DLL.Repositories;
using Domain.Enums;
using Newtonsoft.Json;
using AutoMapper;

namespace BLL.Services
{
    public class AuthenticationService: IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IJwtTokenService _jwtTokenService;
		private readonly IConfiguration _configuration;
		private readonly ISendGridService _sendGridService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;


		public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
			IJwtTokenService jwtTokenService, IConfiguration configuration, ISendGridService sendGridService, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtTokenService = jwtTokenService;
			_configuration = configuration;
			_sendGridService = sendGridService;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
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

		
		public async Task<IEnumerable<CountryVM>> GetAllCountries()
		{
			var countries = await _unitOfWork.CountryRepo.GetAll();
			var list = _mapper.Map<IEnumerable<CountryVM>>(countries);
			return list;

		}

		public async Task<IEnumerable<CityVM>> GetCitiesByCountry(string countryName)
		{
			var country = await _unitOfWork.CountryRepo.Get(c => c.Name == countryName);
			var cities = await _unitOfWork.CityRepo.GetAll(c=> c.CountryId == country.Id);
			var list = _mapper.Map<IEnumerable<CityVM>>(cities);
			return list;
		}

		public async Task<bool> Register(RegisterVM model)
		{

			var existingUser = await _userManager.FindByEmailAsync(model.Email);
			if (existingUser != null)
			{
				throw new Exception("Email already exists");
			}

			var user = new ApplicationUser { UserName = model.Email, Email = model.Email, EmailConfirmed = false, FirstName = model.FirstName, LastName = model.LastName, Country = model.Country, City = model.City };

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				throw new Exception("Error when creating a user");
			}
			else
			{
				result = await _userManager.AddToRoleAsync(user, RoleConstants.AUTHORIZED_USER);
				var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				var code = GenerateRandom6DigitCode();
				user.EmailConfirmationToken = token;
				user.EmailConfirmationCode = code;
				await _userManager.UpdateAsync(user);

				var defaultSettings = new UserPrivacySettings
				{
					UserId = user.Id,
					ProfileViewingValue = (int)ProfileViewingOptions.YourNameAndHeadline,
					EmailVisibilityValue = (int)EmailVisibilityOptions.FirstDegreeConnections,
					ConnectionVisibility = true,
					ShowLastName = true,
					ShareProfileUpdates = true ,
					DiscoverByEmailValue = (int)DiscoverByEmailOptions.Anyone,
					DiscoverByPhoneValue = (int)DiscoverByPhoneOptions.Everyone,
					ActiveStatusVisibilityValue = (int)ActiveStatusVisibilityOptions.AllLinkedInMembers,

				};

				await _unitOfWork.UserPrivacySettingsRepo.Add(defaultSettings);
				await _unitOfWork.SaveAsync();
			}


			return result.Succeeded;
		}
		public async Task<bool> SendConfirmationCode(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				throw new Exception("This email is not registred");
			}


			var frontendURL = _configuration.GetValue<string>("FrontEndURL");
			var callbackURL = $"{frontendURL}/auth/confirm-email?email={email}&token={user.EmailConfirmationToken}";

			string basePath = AppContext.BaseDirectory;
			string htmlFilePath = Path.Combine(basePath, "html", "message-template.html");
			string htmlContent = System.IO.File.ReadAllText(htmlFilePath);

			htmlContent = htmlContent.Replace("{{code}}", user.EmailConfirmationCode);
			htmlContent = htmlContent.Replace("{{email}}", email);
			htmlContent = htmlContent.Replace("{{callbackURL}}", callbackURL);

			string subject = "Confirm Your Email";

			await _sendGridService.SendEmailAsync(email, subject, htmlContent);

			return true;
		}
		public async Task<bool> ConfirmEmail(string userId, string code, string emailToken)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				throw new Exception("User not found");
			}
			if(code== null && emailToken == null)
			{
				throw new Exception("Error");
			}
			if (code != null && code != user.EmailConfirmationCode)
			{
				throw new Exception("Invalid confirmation code");
			}
			if (emailToken != null && code != user.EmailConfirmationToken)
			{
				throw new Exception("Invalid confirmation token");
			}
			var result = await _userManager.ConfirmEmailAsync(user, user.EmailConfirmationToken);
			if (result.Succeeded)
			{
				user.EmailConfirmed = true;
				await _userManager.UpdateAsync(user);

				return true;
			}
			else
			{
				throw new Exception("Failed to confirm email");
			}
		}

		public async Task<LoginResultVM> Login(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return new LoginResultVM { Success = false };
			}
			if (!user.EmailConfirmed)
			{
				await SendConfirmationCode(user.Email);
				throw new Exception("Email not confirmed");

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
						EmailConfirmed = true

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
