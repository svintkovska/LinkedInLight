using BLL.Services;
using DLL.Models;
using LinkedInLight.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedInLight.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly AuthenticationService _authenticationService;

		public AccountController(AuthenticationService authenticationService)
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
				if (result)
				{
					return Ok("Login successful");
				}
				return BadRequest("Login failed");
			}
			catch (Exception ex)
			{
				return Unauthorized("Login failed. " + ex.Message);

			}
		}
	}
}
