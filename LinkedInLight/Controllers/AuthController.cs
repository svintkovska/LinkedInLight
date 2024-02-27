using BLL.Interfaces;
using BLL.Services;
using BLL.DTOs;
using LinkedInLight.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using BLL.ViewModels;

namespace LinkedInLight.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly AuthenticationService _authenticationService;

		public AuthController(AuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			try
			{
				var result = await _authenticationService.Register(model.Email, model.Password);
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
		public async Task<IActionResult> Login(LoginModel model)
		{
			try
			{
				var result = await _authenticationService.Login(model.Email, model.Password);
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
		public async Task<IActionResult> GoogleRegistartion(GoogleModel model)
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
		public async Task<IActionResult> GoogleLogin(GoogleModel model)
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
				return BadRequest("Google login failed");

			}
		}
	}
}
