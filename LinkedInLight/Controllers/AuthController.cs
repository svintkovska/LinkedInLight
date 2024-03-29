﻿using BLL.Interfaces;
using BLL.ViewModels.AuthModels;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using BLL.ViewModels;
using Domain.Models;
using System.Net;

namespace LinkedInLight.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authenticationService;

		public AuthController(IAuthService authenticationService)
		{
			_authenticationService = authenticationService;
		}
		[HttpPost("validate-email")]
		public async Task<IActionResult> ValidateEmail(string email)
		{
			try
			{
				var result = await _authenticationService.IsValidEmail(email);
				if (result)
				{
					return Ok();
				}
				return BadRequest("Email already exists");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("confirm-email")]
		public async Task<IActionResult> ConfirmEmail(string email, string code, string emailToken)
		{
			try
			{
				var result = await _authenticationService.ConfirmEmail(email, code, emailToken);
				if (result)
				{
					return Ok();
				}
				return BadRequest("Failed to confirm email");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("send-code")]
		public async Task<IActionResult> SendConfirmationCode(string email)
		{
			try
			{
				var code = await _authenticationService.SendConfirmationCode(email);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("countries")]
		public async Task<IActionResult> GetAllCountries()
		{
			var countries = await _authenticationService.GetAllCountries();
			return Ok(countries);
		}

		[HttpGet("cities/{country}")]
		public async Task<IActionResult> GetCitiesByCountry(string country)
		{
			var cities = await _authenticationService.GetCitiesByCountry(country);
			return Ok(cities);
		}
		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterVM model)
		{
			try
			{
				var result = await _authenticationService.Register(model);
				if (result)
				{
					return Ok("Registration successful");
				}
				return BadRequest("Registration failed");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		
		[HttpPost("login")]
		public async Task<IActionResult> Login(AuthVM model)
		{
			try
			{
				var result = await _authenticationService.Login(model.Email, model.Password);
				if (result.EmailNotConfirmed)
				{
					return BadRequest($"Your email is not confirmed. A confirmation code was sent to {model.Email}");
				}
				if (result.Success)
				{
					
					return Ok(new { token = result.Token, user = result.User, roles = result.Roles});
				}
				return BadRequest("Login failed");
			}
			catch (Exception ex)
			{
				return Unauthorized("Login failed. " + ex.Message);

			}
		}

		[HttpPost("google/registartion")]
		public async Task<IActionResult> GoogleRegistartion(GoogleVM model)
		{
			try
			{
				var result = await _authenticationService.GoogleRegistration(model);
				if (result.Success)
				{
					return Ok(new { token = result.Token, user = result.User, roles = result.Roles });
				}
				return BadRequest("Google registration failed");

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);

			}
		}

		[HttpPost("google/login")]
		public async Task<IActionResult> GoogleLogin(GoogleVM model)
		{
			try
			{
				var result = await _authenticationService.GoogleLogin(model);
				if (result.Success)
				{
					return Ok(new { token = result.Token, user = result.User, roles = result.Roles });
				}
				return BadRequest("Google login failed");

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);

			}
		}
		[HttpPost("forgotPassword")]
		public async Task<IActionResult> ForgotPassword(string email)
		{
			try
			{
				var result = await _authenticationService.ForgotPassword(email);
				if (result)
				{
					return Ok();
				}
				return BadRequest("Reset password failed");
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("newPassword")]
		public async Task<IActionResult> SetNewPassword(NewPasswordVM model)
		{
			var res = await _authenticationService.SetNewPassword(model);
			if (!res)
			{
				return BadRequest();
			}
			return Ok();
		}
	}
}
