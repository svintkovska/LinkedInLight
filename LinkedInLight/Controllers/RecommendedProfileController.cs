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
	public class RecommendedProfileController : ControllerBase
	{
		private readonly IRecommendedProfileService _recommendedProfileService;

		public RecommendedProfileController(IRecommendedProfileService recommendedProfileService)
		{
			_recommendedProfileService = recommendedProfileService;
		}

		[HttpGet("userCertifications")]
		public async Task<IActionResult> GetUserCertifications()
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var list = await _recommendedProfileService.GetUserCertifications(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("certification/{id}")]
		public async Task<IActionResult> GetCertification(int id)
		{
			try
			{
				var certification = await _recommendedProfileService.GetCertification(id);
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
				await _recommendedProfileService.AddCertification(certification, userId);
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
				await _recommendedProfileService.UpdateCertification(certification);
				return Ok("Certification updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("certification/remove/{id}")]
		public async Task<IActionResult> RemoveCertification(int id)
		{
			try
			{
				await _recommendedProfileService.RemoveCertification(id);
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
				var list = await _recommendedProfileService.GetUserCourses(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("course/{id}")]
		public async Task<IActionResult> GetCourse(int id)
		{
			try
			{
				var certification = await _recommendedProfileService.GetCourse(id);
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

				await _recommendedProfileService.AddCourse(course, userId);
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
				await _recommendedProfileService.UpdateCourse(course);
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
				await _recommendedProfileService.RemoveCourse(courseId);
				return Ok("Course deleted");
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
				var list = await _recommendedProfileService.GetUserProjects(userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("project/{id}")]
		public async Task<IActionResult> GetProject(int id)
		{
			try
			{
				var project = await _recommendedProfileService.GetProject(id);
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
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				await _recommendedProfileService.AddProjectWithContributors(project, userId);
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
				await _recommendedProfileService.UpdateProjectWithContributors(project);
				return Ok("Project updated");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete("project/remove/{id}")]
		public async Task<IActionResult> RemoveProject(int id)
		{
			try
			{
				await _recommendedProfileService.RemoveProject(id);
				return Ok("Project deleted");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
