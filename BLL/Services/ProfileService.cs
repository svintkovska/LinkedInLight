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
	public class ProfileService: IProfileService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IUploadService _uploadService;
		private readonly IUnitOfWork _unitOfWork; 
		public ProfileService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUploadService uploadService,  
			IUnitOfWork unitOfWork)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_uploadService = uploadService;
			_unitOfWork = unitOfWork;
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
			var user =  await _unitOfWork.UserRepo.Get(u=>u.UserName== username);
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

			await _unitOfWork.UserRepo.Save();
			return user;
		}

		public async Task <ApplicationUser> GetUser(string id)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == id);

			return user;
		}

		public async Task<ApplicationUser> EditAbout(string id, string about)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == id);
			user.About = about;
			_unitOfWork.UserRepo.Update(user);
			await _unitOfWork.UserRepo.Save();
			return user;
		}

		public async Task<List<Experience>> GetUserExperiences(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Experience");
			var list = user.Experiences.ToList();
			return user.Experiences.ToList();
		}

		public async Task<Experience> GetExperience(int id)
		{
			var experience = await _unitOfWork.ExperienceRepo.Get(e => e.Id == id);
			return experience;
		}
		public async Task<bool> AddExperience(Experience experience)
		{
			await _unitOfWork.ExperienceRepo.Add(experience);
			await _unitOfWork.ExperienceRepo.Save();
			return true;
		}
		public async Task<bool> RemoveExperience(int experienceId)
		{
			var experience = await _unitOfWork.ExperienceRepo.Get(e => e.Id == experienceId);
			_unitOfWork.ExperienceRepo.Update(experience);
			await _unitOfWork.ExperienceRepo.Save();
			return true;
		}
		public async Task<bool> UpdateExperience(Experience experience)
		{
			_unitOfWork.ExperienceRepo.Update(experience);
			await _unitOfWork.ExperienceRepo.Save();
			return true;
		}

		public async Task<List<Education>> GetUserEducations(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Education");
			var list = user.Educations.ToList();
			return user.Educations.ToList();
		}

		public async Task<Education> GetEducation(int id)
		{
			var education = await _unitOfWork.EducationRepo.Get(e => e.Id == id);
			return education;
		}
		public async Task<bool> AddEducation(Education education)
		{
			await _unitOfWork.EducationRepo.Add(education);
			await _unitOfWork.EducationRepo.Save();
			return true;
		}
		public async Task<bool> RemoveEducation(int educationId)
		{
			var education = await _unitOfWork.EducationRepo.Get(e => e.Id == educationId);
			_unitOfWork.EducationRepo.Update(education);
			await _unitOfWork.EducationRepo.Save();
			return true;
		}
		public async Task<bool> UpdateEducation(Education education)
		{
			_unitOfWork.EducationRepo.Update(education);
			await _unitOfWork.EducationRepo.Save();
			return true;
		}

		public async Task<List<Post>> GetUserPosts(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Post");
			var list = user.Posts.ToList();
			return user.Posts.ToList();
		}
		public async Task<List<Skill>> GetUserSkills(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Skill");
			var list = user.Skills.ToList();
			return user.Skills.ToList();
		}
		public async Task<List<Language>> GetUserLanguages(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Language");
			var list = user.Languages.ToList();
			return user.Languages.ToList();
		}
	}
}
