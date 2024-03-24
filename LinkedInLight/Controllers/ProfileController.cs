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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _profileService.AddLanguage(language, userId);
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
		public async Task<IActionResult> RemoveLanguage(int id)
		{
			try
			{
				await _profileService.RemoveLanguage(id);
				return Ok("Language deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet("userCertifications")]
		public async Task<IActionResult> GetUserCertifications()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var list = await _profileService.GetUserCertifications(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("certification/{id}")]
		public async Task<IActionResult> GetCertification(int experienceId)
		{
			try
			{
				var certification = await _profileService.GetCertification(experienceId);
				return Ok(certification);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("newCertification")]
		public async Task<IActionResult> AddExperience(CertificationVM certification)
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				await _profileService.AddCertification(certification, userId);
				return Ok(certification);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("certification/edit/{id}")]
		public async Task<IActionResult> UpdateCertification(CertificationVM certification)
		{
			try
			{
				await _profileService.UpdateCertification(certification);
				return Ok("Certification updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("certification/remove/{id}")]
		public async Task<IActionResult> RemoveCertification(int certificationId)
		{
			try
			{
				await _profileService.RemoveCertification(certificationId);
				return Ok("Certification deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}



		[HttpGet("userCourses")]
		public async Task<IActionResult> GetUserCourses()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var list = await _profileService.GetUserCourses(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("course/{id}")]
		public async Task<IActionResult> GetCourse(int courseId)
		{
			try
			{
				var certification = await _profileService.GetCourse(courseId);
				return Ok(certification);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("newCourse")]
		public async Task<IActionResult> AddCourse(CourseVM course)
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				await _profileService.AddCourse(course, userId);
				return Ok(course);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("course/edit/{id}")]
		public async Task<IActionResult> UpdateCertification(CourseVM course)
		{
			try
			{
				await _profileService.UpdateCourse(course);
				return Ok("Course updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("course/remove/{id}")]
		public async Task<IActionResult> RemoveCourse(int courseId)
		{
			try
			{
				await _profileService.RemoveCourse(courseId);
				return Ok("Course deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet("userPhoneNumbers")]
		public async Task<IActionResult> GetUserPhoneNumbers()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var list = await _profileService.GetUserPhoneNumbers(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		
		[HttpPost("newPhoneNumber")]
		public async Task<IActionResult> AddPhoneNumber(PhoneNumberVM phoneNumber)
		{
			try
			{
				await _profileService.AddPhoneNumber(phoneNumber);
				return Ok(phoneNumber);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("phoneNumber/edit/{id}")]
		public async Task<IActionResult> UpdateCertification(PhoneNumberVM phoneNumber)
		{
			try
			{
				await _profileService.UpdatePhoneNumber(phoneNumber);
				return Ok("Phone Number updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("phoneNumber/remove/{id}")]
		public async Task<IActionResult> RemovePhoneNumber(int phoneNumberId)
		{
			try
			{
				await _profileService.RemovePhoneNumber(phoneNumberId);
				return Ok("Phone Number deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet("userWebsites")]
		public async Task<IActionResult> GetUserWebsites()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var list = await _profileService.GetUserWebsites(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("newWebsite")]
		public async Task<IActionResult> AddWebsite(WebsiteVM website)
		{
			try
			{
				await _profileService.AddWebsite(website);
				return Ok(website);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("website/edit/{id}")]
		public async Task<IActionResult> UpdateCertification(WebsiteVM website)
		{
			try
			{
				await _profileService.UpdateWebsite(website);
				return Ok("Website Number updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("website/remove/{id}")]
		public async Task<IActionResult> RemoveWebsite(int websiteId)
		{
			try
			{
				await _profileService.RemoveWebsite(websiteId);
				return Ok("Website deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet("userVolunteerExperiences")]
		public async Task<IActionResult> GetUserVolunteerExperiences()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var list = await _profileService.GetUserVolunteerExperiences(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("volunteerExperience/{id}")]
		public async Task<IActionResult> GetVolunteerExperience(int volunteerExperienceId)
		{
			try
			{
				var volunteerExperience = await _profileService.GetVolunteerExperience(volunteerExperienceId);
				return Ok(volunteerExperience);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("newVolunteerExperience")]
		public async Task<IActionResult> AddVolunteerExperience(VolunteerExperienceVM volunteerExperience)
		{
			try
			{
				await _profileService.AddVolunteerExperience(volunteerExperience);
				return Ok(volunteerExperience);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("volunteerExperience/edit/{id}")]
		public async Task<IActionResult> UpdateVolunteerExperience(VolunteerExperienceVM volunteerExperience)
		{
			try
			{
				await _profileService.UpdateVolunteerExperience(volunteerExperience);
				return Ok("Volunteer Experience updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("volunteerExperience/remove/{id}")]
		public async Task<IActionResult> RemoveVolunteerExperience(int volunteerExperienceId)
		{
			try
			{
				await _profileService.RemoveVolunteerExperience(volunteerExperienceId);
				return Ok("Volunteer Experience deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet("userProjects")]
		public async Task<IActionResult> GetUserProjects()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var list = await _profileService.GetUserProjects(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("project/{id}")]
		public async Task<IActionResult> GetProject(int projectId)
		{
			try
			{
				var project = await _profileService.GetProject(projectId);
				return Ok(project);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost("newProject")]
		public async Task<IActionResult> AddProject(ProjectVM project)
		{
			try
			{
				await _profileService.AddProjectWithContributors(project);
				return Ok(project);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("project/edit/{id}")]
		public async Task<IActionResult> UpdateProject(ProjectVM project)
		{
			try
			{
				await _profileService.UpdateProjectWithContributors(project);
				return Ok("Project updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("project/remove/{id}")]
		public async Task<IActionResult> RemoveProject(int projectId)
		{
			try
			{
				await _profileService.RemoveProject(projectId);
				return Ok("Project deleted");
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
