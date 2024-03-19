
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
		public Task<UserProfileVM> EditImage(string userId, string newImage, bool background = false);
		public  Task<UserProfileVM> GetUserProfile(string id);

		public Task<bool> EditAbout(string id, string about);
		public Task<List<ExperienceVM>> GetUserExperiences(string id);
		public Task<ExperienceVM> GetExperience(int id);

		public Task<bool> AddExperience(ExperienceVM experienceVM);
		public Task<bool> RemoveExperience(int experienceId);
		public Task<bool> UpdateExperience(ExperienceVM experienceDTO);

		public Task<List<EducationVM>> GetUserEducations(string id);
		public Task<EducationVM> GetEducation(int id);

		public Task<bool> AddEducation(EducationVM educationDTO);
		public Task<bool> RemoveEducation(int educationId);
		public Task<bool> UpdateEducation(EducationVM educationDTO);
		public Task<List<PostVM>> GetUserPosts(string userid);
		public Task<List<SkillVM>> GetUserSkills(string userid);
		public Task<bool> AddSkill(SkillVM skill);
		public Task<bool> RemoveSkill(int skillId);
		public Task<bool> UpdateSkill(SkillVM skill);
		
		public Task<List<LanguageVM>> GetUserLanguages(string userid);
		public Task<bool> AddLanguage(LanguageVM language);
		public Task<bool> RemoveLanguage(int languageId);
		public Task<bool> UpdateLanguage(LanguageVM language);


		public Task<List<CertificationVM>> GetUserCertifications(string userid);
		public Task<CertificationVM> GetCertification(int id);
		public Task<bool> AddCertification(CertificationVM certification);
		public Task<bool> RemoveCertification(int certificationId);
		public Task<bool> UpdateCertification(CertificationVM certification);

	}
}
