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
		public async Task<IActionResult> GetUser()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				await _profileService.GetUser(userId);
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
		public async Task<ActionResult<ApplicationUser>> EditImage( bool background = false)
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
		public async Task<IActionResult> EditAboutPUT(string about)
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				await _profileService.EditAbout(userId, about);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("userExperiences/{id}")]
		public async Task<IActionResult> GetUserExperiences()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var list = await _profileService.GetUserExperiences(userId);
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
		public async Task<IActionResult> GetUserEducation()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var list = await _profileService.GetUserEducations(userId);
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
		public async Task<IActionResult> GetUserPosts()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				var list = await _profileService.GetUserPosts(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("userSkills/{id}")]
		public async Task<IActionResult> GetUserSkills()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var list = await _profileService.GetUserSkills(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("userLanguages/{id}")]
		public async Task<IActionResult> GetUserLanguages()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var list = await _profileService.GetUserLanguages(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
