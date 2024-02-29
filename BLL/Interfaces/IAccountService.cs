using BLL.DTOs;
using BLL.ViewModels.AuthModels;
using DLL.Data;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IAccountService
	{
		public Task<bool> ChangePassword(ChangePasswordVM model);
		public Task<UserDTO> EditImage(UserDTO userDTO, string username, bool background = false);
		public  Task<UserDTO> GetUser(string id);

		public Task<UserDTO> EditAbout(string id, string about);
		public Task<List<ExperienceDTO>> GetUserExperiences(string id);
		public Task<ExperienceDTO> GetExperience(int id);

		public Task<bool> AddExperience(ExperienceDTO experienceDTO);
		public Task<bool> RemoveExperience(int experienceId);
		public Task<bool> UpdateExperience(ExperienceDTO experienceDTO);

		public Task<List<EducationDTO>> GetUserEducations(string id);
		public Task<EducationDTO> GetEducation(int id);

		public Task<bool> AddEducation(EducationDTO educationDTO);
		public Task<bool> RemoveEducation(int educationId);
		public Task<bool> UpdateEducation(EducationDTO educationDTO);

	}
}
