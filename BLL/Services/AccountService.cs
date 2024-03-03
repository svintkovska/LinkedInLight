using BLL.Interfaces;
using BLL.ViewModels.AuthModels;
using DLL.Data;
using DLL.Repositories.IRepository;
using Domain.Models;
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
		private readonly IApplicationUserRepository _userRepository;
		private readonly IExperience _experienceRepository;
		private readonly IEducation _educationRepository;
		public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUploadService uploadService,  
			IApplicationUserRepository userRepository, IExperience experience, IEducation educationRepository)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_uploadService = uploadService;
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
		public async Task<ApplicationUser> EditImage(ApplicationUser userDTO, string username, bool background = false)
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
			return user;
		}

		public async Task <ApplicationUser> GetUser(string id)
		{
			var user = await _userRepository.Get(u => u.Id == id);

			return user;
		}

		public async Task<ApplicationUser> EditAbout(string id, string about)
		{
			var user = await _userRepository.Get(u => u.Id == id);
			user.About = about;
			_userRepository.Update(user);
			await _userRepository.Save();
			return user;
		}

		public async Task<List<Experience>> GetUserExperiences(string userid)
		{
			var user = await _userRepository.Get(u => u.Id == userid, includeProperties: "Experience");
			var list = user.Experiences.ToList();
			return user.Experiences.ToList();
		}

		public async Task<Experience> GetExperience(int id)
		{
			var experience = await _experienceRepository.Get(e => e.Id == id);
			return experience;
		}
		public async Task<bool> AddExperience(Experience experience)
		{
			await _experienceRepository.Add(experience);
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
		public async Task<bool> UpdateExperience(Experience experience)
		{
			 _experienceRepository.Update(experience);
			await _experienceRepository.Save();
			return true;
		}

		public async Task<List<Education>> GetUserEducations(string userid)
		{
			var user = await _userRepository.Get(u => u.Id == userid, includeProperties: "Education");
			var list = user.Educations.ToList();
			return user.Educations.ToList();
		}

		public async Task<Education> GetEducation(int id)
		{
			var education = await _educationRepository.Get(e => e.Id == id);
			return education;
		}
		public async Task<bool> AddEducation(Education education)
		{
			await _educationRepository.Add(education);
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
		public async Task<bool> UpdateEducation(Education education)
		{
			_educationRepository.Update(education);
			await _educationRepository.Save();
			return true;
		}
	}
}
