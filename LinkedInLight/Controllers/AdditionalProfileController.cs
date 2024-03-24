using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkedInLight.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdditionalProfileController : ControllerBase
	{
		private readonly IAdditionalProfileService _additionalProfileService;

		public AdditionalProfileController(IAdditionalProfileService additionalProfileService)
		{
			_additionalProfileService = additionalProfileService;
		}

		[HttpGet("userLanguages")]
		public async Task<IActionResult> GetUserLanguages()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var list = await _additionalProfileService.GetUserLanguages(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("allLanguages")]
		public async Task<IActionResult> GetAllLanguages()
		{
			try
			{
				var list = await _additionalProfileService.GetAllLanguages();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("newLanguage")]
		public async Task<IActionResult> AddLanguage(UserLanguageVM language)
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				await _additionalProfileService.AddLanguage(language, userId);
				return Ok(language);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("language/edit/{id}")]
		public async Task<IActionResult> UpdateLanguage(UserLanguageVM language)
		{
			try
			{
				await _additionalProfileService.UpdateLanguage(language);
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
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				await _additionalProfileService.RemoveLanguage(id, userId);
				return Ok("Language deleted");
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
				var list = await _additionalProfileService.GetUserPhoneNumbers(userId);
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
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				await _additionalProfileService.AddPhoneNumber(phoneNumber, userId);
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
				await _additionalProfileService.UpdatePhoneNumber(phoneNumber);
				return Ok("Phone Number updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("phoneNumber/remove/{id}")]
		public async Task<IActionResult> RemovePhoneNumber(int id)
		{
			try
			{
				await _additionalProfileService.RemovePhoneNumber(id);
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
				var list = await _additionalProfileService.GetUserWebsites(userId);
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
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				await _additionalProfileService.AddWebsite(website, userId);
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
				await _additionalProfileService.UpdateWebsite(website);
				return Ok("Website Number updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("website/remove/{id}")]
		public async Task<IActionResult> RemoveWebsite(int id)
		{
			try
			{
				await _additionalProfileService.RemoveWebsite(id);
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
				var list = await _additionalProfileService.GetUserVolunteerExperiences(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("volunteerExperience/{id}")]
		public async Task<IActionResult> GetVolunteerExperience(int id)
		{
			try
			{
				var volunteerExperience = await _additionalProfileService.GetVolunteerExperience(id);
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
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				await _additionalProfileService.AddVolunteerExperience(volunteerExperience, userId);
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
				await _additionalProfileService.UpdateVolunteerExperience(volunteerExperience);
				return Ok("Volunteer Experience updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("volunteerExperience/remove/{id}")]
		public async Task<IActionResult> RemoveVolunteerExperience(int id)
		{
			try
			{
				await _additionalProfileService.RemoveVolunteerExperience(id);
				return Ok("Volunteer Experience deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
