using BLL.Interfaces;
using BLL.ViewModels.AuthModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedInLight.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProfileSecurityController : ControllerBase
	{
		private readonly IProfileSecurityService _securityService;
		public ProfileSecurityController(IProfileSecurityService securityService)
		{
			_securityService = securityService;
		}

		[HttpPut("edit/changePassword")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM model)
		{
			try
			{
				var result = await _securityService.ChangePassword(model);
				if (result)
				{
					return Ok();
				}
				return BadRequest("Error when changing password");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);

			}
		}
	}
}
