
using BLL.ViewModels;
using BLL.ViewModels.AuthModels;
using DLL.Data;
using DLL.Repositories;
using DLL.Repositories.IRepository;
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
		public Task<UserProfileVM> GetUserProfile(string id);

		public Task<UserProfileVM> EditImage(string userId, string newImage, bool background = false);
		
		public Task<bool> EditAbout(string id, string about);


		public Task<List<ExperienceVM>> GetUserExperiences(string id);
		public Task<ExperienceVM> GetExperience(int id);
		public Task<bool> AddExperience(ExperienceVM experienceVM, string userid);
		public Task<bool> RemoveExperience(int experienceId);
		public Task<bool> UpdateExperience(ExperienceVM experienceDTO);

		public Task<List<EducationVM>> GetUserEducations(string id);
		public Task<EducationVM> GetEducation(int id);
		public Task<List<IndustryVM>> GetAllIndustries();
		public Task<bool> AddEducation(EducationVM educationDTO, string userid);
		public Task<bool> RemoveEducation(int educationId);
		public Task<bool> UpdateEducation(EducationVM educationDTO);
		public Task<List<PostVM>> GetUserPosts(string userid);


		public Task<List<UserSkillVM>> GetUserSkills(string userid);
		public Task<bool> AddSkill(UserSkillVM skill, string userid);
		public Task<List<UserSkillVM>> GetMainkills(string userid);
		public Task<List<SkillVM>> GetAllSkills();
		public Task<bool> RemoveSkill(int skillId, string userId);
		public Task<bool> UpdateSkill(UserSkillVM skill, string userId);
		

	


		

		



	


		public Task<bool> AddOpenToWork(OpenToWorkVM openToWorkVM, string userId);
		public Task<bool> UpdateOpenToWork(OpenToWorkVM openToWorkVM, string userId);
		public Task<bool> DeleteOpenToWork(string userId);
		public Task<OpenToWorkVM> GetOpenToWorkByUserId(string userId);
	}
}
