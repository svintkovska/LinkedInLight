using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels;
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
		[HttpGet]
		public async Task<IActionResult> GetUserProfile()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var user = await _profileService.GetUserProfile(userId);
				return Ok(user);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		
		[HttpPut("editImage")]
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

		[HttpGet("edit/about")]
		public async Task< IActionResult> EditAbout()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = await _profileService.GetUserProfile(userId);
			return Ok(user.About);
		}

		[HttpPut("edit/about")]
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

		[HttpGet("userExperiences")]
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
		public async Task<IActionResult> AddExperience(ExperienceVM experience)
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
		[HttpPut("experience/edit/{id}")]
		public async Task<IActionResult> UpdateExperience(ExperienceVM experience)
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
		[HttpDelete("experience/remove/{id}")]
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
		[HttpGet("userEducations")]
		public async Task<IActionResult> GetUserEducations()
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
		public async Task<IActionResult> AddEducation(EducationVM education)
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
		[HttpPut("education/edit/{id}")]
		public async Task<IActionResult> UpdateEducation(EducationVM education)
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
		[HttpDelete("education/remove/{id}")]
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

		[HttpGet("userPosts")]
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
		[HttpGet("userSkills")]
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
		[HttpPost("newSkill")]
		public async Task<IActionResult> AddSkill(SkillVM skill)
		{
			try
			{
				await _profileService.AddSkill(skill);
				return Ok(skill);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("skill/edit/{id}")]
		public async Task<IActionResult> UpdateSkill(SkillVM skill)
		{
			try
			{
				await _profileService.UpdateSkill(skill);
				return Ok("Skill updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("skill/remove/{id}")]
		public async Task<IActionResult> RemoveSkill(int skillId)
		{
			try
			{
				await _profileService.RemoveSkill(skillId);
				return Ok("Skill deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("userLanguages")]
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
		[HttpPost("newLanguage")]
		public async Task<IActionResult> AddLanguage(LanguageVM language)
		{
			try
			{
				await _profileService.AddLanguage(language);
				return Ok(language);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("language/edit/{id}")]
		public async Task<IActionResult> UpdateLanguage(LanguageVM language)
		{
			try
			{
				await _profileService.UpdateLanguage(language);
				return Ok("Language updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("language/remove/{id}")]
		public async Task<IActionResult> RemoveLanguage(int languageId)
		{
			try
			{
				await _profileService.RemoveLanguage(languageId);
				return Ok("Language deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
