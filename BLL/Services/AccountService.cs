using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.ViewModels.AuthModels;
using DLL.Data;
using DLL.Models;
using DLL.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class AccountService: IAccountService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IUploadService _uploadService;
		private readonly IMapper _mapper;
		private readonly IApplicationUserRepository _userRepository;
		private readonly IExperience _experienceRepository;
		private readonly IEducation _educationRepository;
		public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUploadService uploadService, IMapper mapper, 
			IApplicationUserRepository userRepository, IExperience experience, IEducation educationRepository)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_uploadService = uploadService;
			_mapper = mapper;
			_userRepository = userRepository;
			_experienceRepository = experience;
			_educationRepository = educationRepository;
		}

		public async Task<bool> ChangePassword( ChangePasswordVM model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				throw new Exception("User not found");
			}
			var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
			if (!result.Succeeded)
			{
				throw new Exception("Error when changing password");
			}
			return result.Succeeded;

		}
		public async Task<UserDTO> EditImage(UserDTO userDTO, string username, bool background = false)
		{
			var user =  await _userRepository.Get(u=>u.UserName== username);
			if (!background)
			{
				if (!string.IsNullOrEmpty(userDTO.Image) && userDTO.Image.Split(',').Length == 2)
				{
					if (user.Image != null)
					{
						_uploadService.RemoveImage(user.Image);
					}
					user.Image = _uploadService.SaveImageFromBase64(userDTO.Image);
				}

				if (string.IsNullOrEmpty(userDTO.Image) && user.Image != null)
				{
					_uploadService.RemoveImage(user.Image);
					user.Image = null;
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(userDTO.Background) && userDTO.Background.Split(',').Length == 2)
				{
					if (user.Background != null)
					{
						_uploadService.RemoveImage(user.Background);
					}
					user.Background = _uploadService.SaveImageFromBase64(userDTO.Background);
				}

				if (string.IsNullOrEmpty(userDTO.Background) && user.Background != null)
				{
					_uploadService.RemoveImage(user.Background);
					user.Background = null;
				}
			}

			await _userRepository.Save();
			return _mapper.Map<UserDTO>(user);
		}

		public async Task <UserDTO> GetUser(string id)
		{
			var user = await _userRepository.Get(u => u.Id == id);

			return _mapper.Map<UserDTO>(user);
		}

		public async Task<UserDTO> EditAbout(string id, string about)
		{
			var user = await _userRepository.Get(u => u.Id == id);
			user.About = about;
			_userRepository.Update(user);
			await _userRepository.Save();
			return _mapper.Map<UserDTO>(user);
		}

		public async Task<List<ExperienceDTO>> GetUserExperiences(string userid)
		{
			var user = await _userRepository.Get(u => u.Id == userid, includeProperties: "Experience");
			var list = user.Experiences.ToList();
			return _mapper.Map<List<ExperienceDTO>>(user.Experiences);
		}

		public async Task<ExperienceDTO> GetExperience(int id)
		{
			var experience = await _experienceRepository.Get(e => e.Id == id);
			return _mapper.Map<ExperienceDTO>(experience);
		}
		public async Task<bool> AddExperience(ExperienceDTO experienceDTO)
		{
			await _experienceRepository.Add(_mapper.Map<Experience>(experienceDTO));
			await _experienceRepository.Save();
			return true;
		}
		public async Task<bool> RemoveExperience(int experienceId)
		{
			var experience = await _experienceRepository.Get(e => e.Id == experienceId);
			_experienceRepository.Update(experience);
			await _experienceRepository.Save();
			return true;
		}
		public async Task<bool> UpdateExperience(ExperienceDTO experienceDTO)
		{
			 _experienceRepository.Update(_mapper.Map<Experience>(experienceDTO));
			await _experienceRepository.Save();
			return true;
		}

		public async Task<List<EducationDTO>> GetUserEducations(string userid)
		{
			var user = await _userRepository.Get(u => u.Id == userid, includeProperties: "Education");
			var list = user.Educations.ToList();
			return _mapper.Map<List<EducationDTO>>(user.Educations);
		}

		public async Task<EducationDTO> GetEducation(int id)
		{
			var education = await _educationRepository.Get(e => e.Id == id);
			return _mapper.Map<EducationDTO>(education);
		}
		public async Task<bool> AddEducation(EducationDTO educationDTO)
		{
			await _educationRepository.Add(_mapper.Map<Education>(educationDTO));
			await _educationRepository.Save();
			return true;
		}
		public async Task<bool> RemoveEducation(int educationId)
		{
			var education = await _educationRepository.Get(e => e.Id == educationId);
			_educationRepository.Update(education);
			await _educationRepository.Save();
			return true;
		}
		public async Task<bool> UpdateEducation(EducationDTO educationDTO)
		{
			_educationRepository.Update(_mapper.Map<Education>(educationDTO));
			await _educationRepository.Save();
			return true;
		}
	}
}
