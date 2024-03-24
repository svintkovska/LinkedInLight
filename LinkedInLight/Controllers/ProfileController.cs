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
		public async Task<ActionResult<ApplicationUser>> EditImage([FromForm] string newImage, bool background = false)
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var result = await _profileService.EditImage(userId, newImage, background);
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
		public async Task<IActionResult> EditAboutPUT([FromBody] string about)
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
		public async Task<IActionResult> GetExperience(int id)
		{
			try
			{
				var experience = await _profileService.GetExperience(id);
				return Ok(experience);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("allIndustries")]
		public async Task<IActionResult> GetAllIndustries()
		{
			try
			{
				var list = await _profileService.GetAllIndustries();
				return Ok(list);
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _profileService.AddExperience(experience, userId);
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
		public async Task<IActionResult> RemoveExperience(int id)
		{
			try
			{
				await _profileService.RemoveExperience(id);
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
		public async Task<IActionResult> GetEducation(int id)
		{
			try
			{
				var education = await _profileService.GetEducation(id);
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _profileService.AddEducation(education, userId);
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
		public async Task<IActionResult> RemoveEducation(int id)
		{
			try
			{
				await _profileService.RemoveEducation(id);
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
		[HttpGet("mainSkills")]
		public async Task<IActionResult> GetMainkills()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var list = await _profileService.GetMainkills(userId);
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _profileService.AddSkill(skill, userId);
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
		public async Task<IActionResult> RemoveSkill(int id)
		{
			try
			{
				await _profileService.RemoveSkill(id);
				return Ok("Skill deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


	
		[HttpPost("addOpenToWork")]
		public async Task<IActionResult> AddOpenToWork(OpenToWorkVM openToWorkVM)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _profileService.AddOpenToWork(openToWorkVM, userId);
			if (result)
			{
				return Ok("OpenToWork added successfully");
			}
			return BadRequest("Failed to add OpenToWork");
		}

		[HttpPut("updateOpenToWorkVM")]
		public async Task<IActionResult> UpdateOpenToWork(OpenToWorkVM openToWorkVM)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _profileService.UpdateOpenToWork(openToWorkVM, userId);
			if (result)
			{
				return Ok("OpenToWork updated successfully");
			}
			return BadRequest("Failed to update OpenToWork");
		}

		[HttpDelete("deleteOpenToWorkVM")]
		public async Task<IActionResult> DeleteOpenToWork()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _profileService.DeleteOpenToWork(userId);
			if (result)
			{
				return Ok("OpenToWork deleted successfully");
			}
			return BadRequest("Failed to delete OpenToWork");
		}

		[HttpGet("getOpenToWorkVM")]
		public async Task<IActionResult> GetOpenToWork()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var openToWorkVM = await _profileService.GetOpenToWorkByUserId(userId);
			if (openToWorkVM != null)
			{
				return Ok(openToWorkVM);
			}
			return NotFound("OpenToWork not found");
		}

	}
}
