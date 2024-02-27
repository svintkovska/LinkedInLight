using BLL.Services;
using BLL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkedInLight.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly AccountService _accountService;
		public AccountController(AccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost("edit/changePassword")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
		{
			try
			{
				var result = await _accountService.ChangePassword(model);
				if (result)
				{
					return Ok();
				}
				return BadRequest(result);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);

			}		
		}
	}
}
