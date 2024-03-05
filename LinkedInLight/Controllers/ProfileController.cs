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
using System.Xml.Linq;

namespace LinkedInLight.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize]
	public class ProfileController : ControllerBase
	{
		private readonly IProfileService _profileService;
		public ProfileController(IProfileService profileService)
		{
			_profileService = profileService;
		}
		[HttpGet("")]
		public async Task<IActionResult> GetUser(string id)
		{
			try
			{
				await _profileService.GetUser(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("edit/changePassword")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM model)
		{
			try
			{
				var result = await _profileService.ChangePassword(model);
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
		public async Task<ActionResult<ApplicationUser>> EditImage(string userId, bool background = false)
		{
			try
			{
				var result = await _profileService.EditImage(userId, background);
				if (result != null)
				{
					return Ok(result);
				}
				return BadRequest("Error when changing image");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);

			}			
		}

		[HttpGet("edit/about/{id}")]
		public async Task< IActionResult> EditAbout(string id)
		{
			var user = await _profileService.GetUser(id);
			return Ok(user.About);
		}

		[HttpPut("edit/about/{id}")]
		public async Task<IActionResult> EditAbout(string id, string about)
		{
			try
			{
				await _profileService.EditAbout(id, about);
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
				var list = await _profileService.GetUserExperiences(userid);
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
				var experience = await _profileService.GetExperience(experienceId);
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
				 await _profileService.AddExperience(experience);
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
				await _profileService.UpdateExperience(experience);
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
				await _profileService.RemoveExperience(experienceId);
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
				var list = await _profileService.GetUserEducations(userid);
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
				var education = await _profileService.GetEducation(educationId);
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
				await _profileService.AddEducation(education);
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
				await _profileService.UpdateEducation(education);
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
				await _profileService.RemoveEducation(educationId);
				return Ok("Education deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("userPosts/{id}")]
		public async Task<IActionResult> GetUserPosts(string userid)
		{
			try
			{
				var list = await _profileService.GetUserPosts(userid);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("userSkills/{id}")]
		public async Task<IActionResult> GetUserSkills(string userid)
		{
			try
			{
				var list = await _profileService.GetUserSkills(userid);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("userLanguages/{id}")]
		public async Task<IActionResult> GetUserLanguages(string userid)
		{
			try
			{
				var list = await _profileService.GetUserLanguages(userid);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
