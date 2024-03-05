
using BLL.ViewModels.AuthModels;
using DLL.Data;
using DLL.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IProfileService
	{
		public Task<bool> ChangePassword(ChangePasswordVM model);
		public Task<ApplicationUser> EditImage(string userId, bool background = false);
		public  Task<ApplicationUser> GetUser(string id);

		public Task<ApplicationUser> EditAbout(string id, string about);
		public Task<List<Experience>> GetUserExperiences(string id);
		public Task<Experience> GetExperience(int id);

		public Task<bool> AddExperience(Experience experienceDTO);
		public Task<bool> RemoveExperience(int experienceId);
		public Task<bool> UpdateExperience(Experience experienceDTO);

		public Task<List<Education>> GetUserEducations(string id);
		public Task<Education> GetEducation(int id);

		public Task<bool> AddEducation(Education educationDTO);
		public Task<bool> RemoveEducation(int educationId);
		public Task<bool> UpdateEducation(Education educationDTO);
		public Task<List<Post>> GetUserPosts(string userid);
		public Task<List<Skill>> GetUserSkills(string userid);
		public Task<List<Language>> GetUserLanguages(string userid);




	}
}
