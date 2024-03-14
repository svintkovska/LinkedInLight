using BLL.Interfaces;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkedInLight.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PrivacySettingsController : ControllerBase
	{
		private readonly IPrivacySettingsService _settingsService;
		public PrivacySettingsController(IPrivacySettingsService settingsService)
		{
			_settingsService = settingsService;
		}
		[HttpGet("ProfileViewing")]
		public async Task<ActionResult<ProfileViewingOptions>> GetProfileViewing()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.GetProfileViewing(userId);
			return Ok(result);
		}
		[HttpPost("updateProfileViewing")]
		public async Task<IActionResult> UpdateProfileViewing(int profileViewingValue)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.UpdateProfileViewinValue(userId, profileViewingValue);
			if (result)
			{
				return Ok("Profile viewing updated successfully");
			}
			return BadRequest(result);
		}
		[HttpGet("emailVisibility")]
		public async Task<ActionResult<EmailVisibilityOptions>> GetEmailVisibility()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.GetEmailVisibility(userId);
			return Ok(result);
		}

		[HttpPost("updateEmailVisibility")]
		public async Task<IActionResult> UpdateEmailVisibility(int emailVisibilityValue)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.UpdateEmailVisibilityValue(userId, emailVisibilityValue);
			if (result)
			{
				return Ok("Email visibility updated successfully");
			}
			return BadRequest(result);
		}
		[HttpGet("DiscoverByEmail")]
		public async Task<ActionResult<DiscoverByEmailOptions>> GetDiscoverByEmail()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.GetDiscoverByEmail(userId);
			return Ok(result);
		}
		[HttpPost("updateDiscoverByEmail")]
		public async Task<IActionResult> UpdateDiscoverByEmail (int discoverByEmailValue)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.UpdateDiscoverByEmailValue(userId, discoverByEmailValue);
			if (result)
			{
				return Ok("Discover by email updated successfully");
			}
			return NotFound("User not found");
		}
		[HttpGet("DiscoverByPhone")]
		public async Task<ActionResult<DiscoverByPhoneOptions>> GetDiscoverByPhone()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.GetDiscoverByPhone(userId);
			return Ok(result);
		}

		[HttpPost("updateDiscoverByPhone")]
		public async Task<IActionResult> UpdateDiscoverByPhone( int discoverByPhoneValue)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.UpdateDiscoverByPhoneValue(userId, discoverByPhoneValue);
			if (result)
			{
				return Ok("Discover by phone updated successfully");
			}
			return NotFound("User not found");
		}
		[HttpGet("activeStatusVisibility")]
		public async Task<ActionResult<ActiveStatusVisibilityOptions>> GetActiveStatusVisibility()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.GetActiveStatusVisibility(userId);
			return Ok(result);
		}

		[HttpPost("updateActiveStatusVisibility")]
		public async Task<IActionResult> UpdateActiveStatusVisibility( int activeStatusVisibilityValue)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var result = await _settingsService.UpdateActiveStatusVisibilityValue(userId, activeStatusVisibilityValue);
			if (result)
			{
				return Ok("Active status visibility updated successfully");
			}
			return NotFound("User not found");
		}
		[HttpGet("connectionVisibility")]
		public async Task<ActionResult> GetConnectionVisibility()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.GetConnectionVisibility(userId);
			return Ok(result);
		}

		[HttpPost("updateConnectionVisibility")]
		public async Task<IActionResult> UpdateConnectionVisibility(bool connectionVisibility)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.UpdateConnectionVisibility(userId, connectionVisibility);
			if (result)
			{
				return Ok("Connection visibility updated successfully");
			}
			return NotFound("User not found");
		}
		[HttpGet("showLastName")]
		public async Task<ActionResult> GetShowLastName()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.GetShowLastName(userId);
			return Ok(result);
		}

		[HttpPost("updateShowLastName")]
		public async Task<IActionResult> UpdateShowLastName( bool showLastName)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.UpdateShowLastName(userId, showLastName);
			if (result)
			{
				return Ok("Show last name updated successfully");
			}
			return NotFound("User not found");
		}
		[HttpGet("shareProfileUpdates")]
		public async Task<ActionResult> GetShareProfileUpdates()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.GetShareProfileUpdates(userId);
			return Ok(result);
		}

		[HttpPost("updateShareProfileUpdates")]
		public async Task<IActionResult> UpdateShareProfileUpdates(bool shareProfileUpdates)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.UpdateShareProfileUpdates(userId, shareProfileUpdates);
			if (result)
			{
				return Ok("Share profile updates updated successfully");
			}
			return NotFound("User not found");
		}

		[HttpGet("enums")]
		public ActionResult<Dictionary<string, IEnumerable<string>>> GetAllEnumValues()
		{
			var enumValues = _settingsService.GetAllEnumValues();
			return Ok(enumValues);
		}
		[HttpDelete("blockedUsers/{blockedUserId}")]
		public async Task<ActionResult> RemoveBlockedUser(string blockedUserId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _settingsService.RemoveBlockedUser(userId, blockedUserId);
			if (!result)
			{
				return NotFound("Blocked user not found.");
			}

			return NoContent();
		}

		[HttpGet("blockedUsers")]
		public async Task<ActionResult<IEnumerable<BlockedUser>>> GetBlockedUsers()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var blockedUsers = await _settingsService.GetBlockedUsers(userId);
			return Ok(blockedUsers);
		}








	}
}
