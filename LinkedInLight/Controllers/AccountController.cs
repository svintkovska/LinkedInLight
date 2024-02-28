using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels.AuthModels;
using Google.Apis.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkedInLight.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost("edit/changePassword")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM model)
		{
			try
			{
				var result = await _accountService.ChangePassword(model);
				if (result)
				{
					return Ok();
				}
				return BadRequest("Error when changing password");
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);

			}		
		}
		[HttpPost("editImage")]
		public async Task<ActionResult<UserDTO>> Edit(UserDTO user, bool background = false)
		{
			string name = User.FindFirstValue(ClaimTypes.Name);
			return Ok(await _accountService.EditImage(user, name, background));
			
		}
	}
}
