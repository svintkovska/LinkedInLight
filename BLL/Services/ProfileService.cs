using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels;
using BLL.ViewModels.AuthModels;
using DLL.Data;
using DLL.Repositories;
using DLL.Repositories.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class ProfileService: IProfileService
	{

		protected readonly IUploadService _uploadService;
		protected readonly IUnitOfWork _unitOfWork;
		protected readonly IMapper _mapper;

		public ProfileService(IUploadService uploadService, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_uploadService = uploadService;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

	
		public async Task<UserProfileVM> EditImage(string userId, string newImage, bool background = false)
		{
			var user =  await _unitOfWork.UserRepo.Get(u=>u.Id== userId);
			if (!background)
			{

				if (!string.IsNullOrEmpty(newImage) && newImage.Split(',').Length == 2)
				{
					if (user.Image != null)
					{
						_uploadService.RemoveImage(user.Image);
					}
					user.Image = _uploadService.SaveImageFromBase64(newImage);
				}

				if (string.IsNullOrEmpty(newImage) && user.Image != null)
				{
					_uploadService.RemoveImage(user.Image);
					user.Image = null;
				}


			}
			else
			{
				if (!string.IsNullOrEmpty(newImage) && newImage.Split(',').Length == 2)
				{
					if (user.Background != null)
					{
						_uploadService.RemoveImage(user.Background);
					}
					user.Background = _uploadService.SaveImageFromBase64(newImage);
				}

				if (string.IsNullOrEmpty(newImage) && user.Background != null)
				{
					_uploadService.RemoveImage(user.Background);
					user.Background = null;
                }
            }

			_unitOfWork.UserRepo.Update(user);

			await _unitOfWork.SaveAsync();

			var userProfile = await GetUserProfile(user.Id);
			return userProfile;
		}

		public async Task<UserProfileVM> GetUserProfile(string id)
		{
			var user = await _unitOfWork.UserRepo.GetUserProfileProps(id);

			var userProfile = _mapper.Map<ApplicationUser, UserProfileVM>(user);

			return userProfile;
		}

		public async Task<bool> EditAbout(string id, string about)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == id);
			if(user != null)
			{
				user.About = about;
				_unitOfWork.UserRepo.Update(user);
				await _unitOfWork.SaveAsync();
				return true;
			}
			return false;
		}

		public async Task<List<ExperienceVM>> GetUserExperiences(string userid)
		{
			var experienceList = await _unitOfWork.ExperienceRepo.GetUserExperiencesWithIndustry(userid);
			var list = _mapper.Map<List<ExperienceVM>>(experienceList);
			return list;
		}
		public async Task<ExperienceVM> GetExperience(int id)
		{
			var exp = await _unitOfWork.ExperienceRepo.Get(e => e.Id == id, includeProperties: "Industry");
			var experience = _mapper.Map<ExperienceVM>(exp);
			return experience;
		}
		public async Task<List<IndustryVM>> GetAllIndustries()
		{
			var industries = await _unitOfWork.IndustryRepo.GetAll();
			var industriesVMs = _mapper.Map<List<IndustryVM>>(industries);

			return industriesVMs;
		}
		public async Task<bool> AddExperience(ExperienceVM experience, string userid)
		{
			var mappedExperience = _mapper.Map<Experience>(experience);
			mappedExperience.ApplicationUserId = userid;

			var industry = await _unitOfWork.IndustryRepo.Get(val => val.Name == experience.Industry.Name);
			if (industry == null)
			{
				return false;
			}

			mappedExperience.IndustryId = industry.Id;

			await _unitOfWork.ExperienceRepo.Add(mappedExperience);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveExperience(int experienceId)
		{
			var experience = await _unitOfWork.ExperienceRepo.Get(e => e.Id == experienceId);
			_unitOfWork.ExperienceRepo.Remove(experience);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateExperience(ExperienceVM experience)
		{
			var existingExperience = await _unitOfWork.ExperienceRepo.Get(e => e.Id == experience.Id);
			if (existingExperience == null)
			{
				return false;
			}

			existingExperience.Title = experience.Title;
			existingExperience.CompanyName = experience.CompanyName;
			existingExperience.Description = experience.Description;
			existingExperience.StartDate = experience.StartDate;
			existingExperience.EndDate = experience.EndDate;
			existingExperience.CurrentlyWorking = experience.CurrentlyWorking;
			existingExperience.ProfileHeadline = experience.ProfileHeadline;

			var industry = await _unitOfWork.IndustryRepo.Get(val => val.Name == experience.Industry.Name);
			if (industry == null)
			{
				return false;
			}

			existingExperience.IndustryId = industry.Id;

			_unitOfWork.ExperienceRepo.Update(existingExperience);
			await _unitOfWork.SaveAsync();
			return true;
		}

		public async Task<List<EducationVM>> GetUserEducations(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Educations");
			var educationList = user.Educations.ToList();
			var list = _mapper.Map<List<EducationVM>>(educationList);
			return list;
		}
		public async Task<EducationVM> GetEducation(int id)
		{
			var edu = await _unitOfWork.EducationRepo.Get(e => e.Id == id);
			var education = _mapper.Map<EducationVM>(edu);
			return education;
		}
		public async Task<bool> AddEducation(EducationVM education, string userid)
		{
			var mappedEducation = _mapper.Map<Education>(education);
			mappedEducation.ApplicationUserId = userid;

            await _unitOfWork.EducationRepo.Add(mappedEducation);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveEducation(int educationId)
		{
			var education = await _unitOfWork.EducationRepo.Get(e => e.Id == educationId);
			_unitOfWork.EducationRepo.Remove(education);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateEducation(EducationVM education)
		{
			var existingEducation = await _unitOfWork.EducationRepo.Get(e => e.Id == education.Id);
			if (existingEducation == null)
			{
				return false;
			}

			existingEducation.Description = education.Description;
			existingEducation.StartDate = education.StartDate;
			existingEducation.EndDate = education.EndDate;
			existingEducation.CurrentlyStudying= education.CurrentlyStudying;
			existingEducation.Degree = education.Degree;
			existingEducation.School = education.School;
			existingEducation.Grade = education.Grade;
			existingEducation.FieldOfStudy = education.FieldOfStudy;

			_unitOfWork.EducationRepo.Update(existingEducation);
			await _unitOfWork.SaveAsync();
			return true;
		}

		public async Task<List<PostVM>> GetUserPosts(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Posts");
			var postList = user.Posts.ToList();
			var list = _mapper.Map<List<PostVM>>(postList);

			return list;
		}

		public async Task<List<UserSkillVM>> GetUserSkills(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "UserSkills.Skill");
			var skillList = user.UserSkills.ToList();
			var list  = _mapper.Map<List<UserSkillVM>>(skillList);

			return list;
		}
		public async Task<List<UserSkillVM>> GetMainkills(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "UserSkills.Skill");
			var mainSkillList = user.UserSkills.Where(s => s.IsMainSkill).ToList();
			var list = _mapper.Map<List<UserSkillVM>>(mainSkillList);
			return list;
		}
		public async Task<List<SkillVM>> GetAllSkills()
		{
			var skills = await _unitOfWork.SkillRepo.GetAll();
			var skillVMs = _mapper.Map<List<SkillVM>>(skills);

			return skillVMs;
		}
		public async Task<bool> AddSkill(UserSkillVM skill, string userid)
		{
			var selectedSkill= await _unitOfWork.SkillRepo.Get(l => l.Name.ToLower() == skill.Skill.Name.ToLower());
			if (skill.IsMainSkill)
			{
				var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "UserSkills");
				var mainSkillCount = user.UserSkills.Count(s => s.IsMainSkill);

				if (mainSkillCount >= 5)
				{
					throw new Exception("You can only have 5 main skills.");
				}
			}
			if (selectedSkill == null)
			{
				var newSkill = new Skill { Name = skill.Skill.Name };
				await _unitOfWork.SkillRepo.Add(newSkill);
				await _unitOfWork.SaveAsync();

				selectedSkill = newSkill;
			}

			var userSkill = new UserSkill
			{
				SkillId = selectedSkill.Id,
				ApplicationUserId = userid,
				IsMainSkill = skill.IsMainSkill
			};

			await _unitOfWork.UserSkillRepo.Add(userSkill);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveSkill(int skillId, string userId)
		{
			var skill = await _unitOfWork.UserSkillRepo.Get(s => s.Id == skillId && s.ApplicationUserId == userId);
			if (skill == null)
			{
				return false;
			}
			_unitOfWork.UserSkillRepo.Remove(skill);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateSkill(UserSkillVM skill, string userId)
		{
			var existingSkill = await _unitOfWork.UserSkillRepo.Get(e => e.Id == skill.Id);
			if (existingSkill == null)
			{
				return false;
			}
			if (skill.IsMainSkill)
			{
				var user = await _unitOfWork.UserRepo.Get(u => u.Id == userId, includeProperties: "UserSkills");
				var mainSkillCount = user.UserSkills.Count(s => s.IsMainSkill);

				if (mainSkillCount >= 5)
				{
					throw new Exception("You can only have 5 main skills.");
				}
			}
			var updatedSkill = await _unitOfWork.SkillRepo.Get(l => l.Id == existingSkill.Id);
			if (updatedSkill == null)
			{
				updatedSkill = new Skill { Name = skill.Skill.Name };
				await _unitOfWork.SkillRepo.Add(updatedSkill);
				await _unitOfWork.SaveAsync();
			}

			existingSkill.SkillId = updatedSkill.Id;
			existingSkill.IsMainSkill = skill.IsMainSkill;

			_unitOfWork.UserSkillRepo.Update(existingSkill);
			await _unitOfWork.SaveAsync();
			return true;
		}




		public async Task<bool> AddOpenToWork(OpenToWorkVM openToWorkVM, string userId)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userId);
			user.OpenToWork = true;
			var openToWork = _mapper.Map<OpenToWork>(openToWorkVM);
			openToWork.ApplicationUserId= userId;

			foreach (var positionVM in openToWorkVM.OpenToWorkPositions)
			{
				openToWork.OpenToWorkPositions.Add(new OpenToWorkPosition
				{
					OpenToWork = openToWork,
					PositionId = positionVM.PositionId
				});
			}

			foreach (var cityVM in openToWorkVM.OpenToWorkCities)
			{
				openToWork.OpenToWorkCities.Add(new OpenToWorkCity
				{
					OpenToWork = openToWork,
					CityId = cityVM.CityId
				});
			}

			foreach (var countryVM in openToWorkVM.OpenToWorkCountries)
			{
				openToWork.OpenToWorkCountries.Add(new OpenToWorkCountry
				{
					OpenToWork = openToWork,
					CountryId = countryVM.CountryId
				});
			}

			await _unitOfWork.OpenToWorkRepo.Add(openToWork);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateOpenToWork(OpenToWorkVM openToWorkVM, string userId)
		{
			var existingOpenToWork = await _unitOfWork.OpenToWorkRepo.Get(o => o.ApplicationUserId == userId);
			if (existingOpenToWork == null)
			{
				return false;
			}

			existingOpenToWork.CanStartImmediately = openToWorkVM.CanStartImmediately;
			existingOpenToWork.FullTime = openToWorkVM.FullTime;
			existingOpenToWork.PartTime = openToWorkVM.PartTime;
			existingOpenToWork.Internship = openToWorkVM.Internship;
			existingOpenToWork.Contract = openToWorkVM.Contract;
			existingOpenToWork.Temporary = openToWorkVM.Temporary;
			existingOpenToWork.VisibleForAll = openToWorkVM.VisibleForAll;

			_unitOfWork.OpenToWorkPositionRepo.RemoveRange(existingOpenToWork.OpenToWorkPositions);

			foreach (var positionVM in openToWorkVM.OpenToWorkPositions)
			{
				existingOpenToWork.OpenToWorkPositions.Add(new OpenToWorkPosition
				{
					OpenToWork = existingOpenToWork,
					PositionId = positionVM.PositionId
				});
			}

			_unitOfWork.OpenToWorkCityRepo.RemoveRange(existingOpenToWork.OpenToWorkCities);

			foreach (var cityVM in openToWorkVM.OpenToWorkCities)
			{
				existingOpenToWork.OpenToWorkCities.Add(new OpenToWorkCity
				{
					OpenToWork = existingOpenToWork,
					CityId = cityVM.CityId
				});
			}

			_unitOfWork.OpenToWorkCountryRepo.RemoveRange(existingOpenToWork.OpenToWorkCountries);

			foreach (var countryVM in openToWorkVM.OpenToWorkCountries)
			{
				existingOpenToWork.OpenToWorkCountries.Add(new OpenToWorkCountry
				{
					OpenToWork = existingOpenToWork,
					CountryId = countryVM.CountryId
				});
			}

			_unitOfWork.OpenToWorkRepo.Update(existingOpenToWork);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> DeleteOpenToWork(string userId)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userId);
			user.OpenToWork = false;
			var existingOpenToWork = await _unitOfWork.OpenToWorkRepo.Get(o => o.ApplicationUserId == userId, includeProperties: "OpenToWorkCities,OpenToWorkCountries,OpenToWorkPositions");
			if (existingOpenToWork == null)
			{
				return false;
			}

			_unitOfWork.OpenToWorkCityRepo.RemoveRange(existingOpenToWork.OpenToWorkCities);
			_unitOfWork.OpenToWorkCountryRepo.RemoveRange(existingOpenToWork.OpenToWorkCountries);
			_unitOfWork.OpenToWorkPositionRepo.RemoveRange(existingOpenToWork.OpenToWorkPositions);
			_unitOfWork.OpenToWorkRepo.Remove(existingOpenToWork);

			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<OpenToWorkVM> GetOpenToWorkByUserId(string userId)
		{
			var openToWork = await _unitOfWork.OpenToWorkRepo.Get(
				o => o.ApplicationUserId == userId,
				includeProperties: "OpenToWorkPositions,OpenToWorkCities,OpenToWorkCountries"
			);
			var openToWorkVM = _mapper.Map<OpenToWorkVM>(openToWork);
			return openToWorkVM;
		}

	}

}
