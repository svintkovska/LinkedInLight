using BLL.Interfaces;
using BLL.Services;
using BLL.DTOs;
using LinkedInLight.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace LinkedInLight.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly AuthenticationService _authenticationService;
		private readonly IJwtTokenService _jwtTokenService;

		public AccountController(AuthenticationService authenticationService, IJwtTokenService jwtTokenService)
		{
			_authenticationService = authenticationService;
			_jwtTokenService = jwtTokenService;
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
					
					return Ok(new { token = result.Token, user = result.User, roles = result.User});
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
