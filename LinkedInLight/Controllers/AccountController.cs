using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels.AuthModels;
using Domain.Models;
using Google.Apis.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
		public async Task<ActionResult<ApplicationUser>> Edit(ApplicationUser user, bool background = false)
		{
			string name = User.FindFirstValue(ClaimTypes.Name);
			return Ok(await _accountService.EditImage(user, name, background));
			
		}

		[HttpGet("edit/about/{id}")]
		public async Task< IActionResult> EditAbout(string id)
		{
			var user = await _accountService.GetUser(id);
			return Ok(user.About);
		}

		[HttpPut("edit/about/{id}")]
		public async Task<IActionResult> EditAbout(string id, string about)
		{
			try
			{
				await _accountService.EditAbout(id, about);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("userExperiences/{id}")]
		public async Task<IActionResult> GetUserExperiences(string userid)
		{
			try
			{
				var list = await _accountService.GetUserExperiences(userid);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("experience/{id}")]
		public async Task<IActionResult> GetExperience(int experienceId)
		{
			try
			{
				var experience = await _accountService.GetExperience(experienceId);
				return Ok(experience);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("newExperience")]
		public async Task<IActionResult> AddExperience(Experience experience)
		{
			try
			{
				 await _accountService.AddExperience(experience);
				return Ok(experience);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("experience/edit/{id}")]
		public async Task<IActionResult> UpdateExperience(Experience experience)
		{
			try
			{
				await _accountService.UpdateExperience(experience);
				return Ok("Experience updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("experience/remove/{id}")]
		public async Task<IActionResult> RemoveExperience(int experienceId)
		{
			try
			{
				await _accountService.RemoveExperience(experienceId);
				return Ok("Experience deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet("userEducation/{id}")]
		public async Task<IActionResult> GetUserEducation(string userid)
		{
			try
			{
				var list = await _accountService.GetUserEducations(userid);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("education/{id}")]
		public async Task<IActionResult> GetEducation(int educationId)
		{
			try
			{
				var education = await _accountService.GetEducation(educationId);
				return Ok(education);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("newEducation")]
		public async Task<IActionResult> AddEducation(Education education)
		{
			try
			{
				await _accountService.AddEducation(education);
				return Ok(education);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("education/edit/{id}")]
		public async Task<IActionResult> UpdateEducation(Education education)
		{
			try
			{
				await _accountService.UpdateEducation(education);
				return Ok("Education updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("education/remove/{id}")]
		public async Task<IActionResult> RemoveEducation(int educationId)
		{
			try
			{
				await _accountService.RemoveEducation(educationId);
				return Ok("Education deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
