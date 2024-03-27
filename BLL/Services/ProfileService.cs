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
			var experienceList = await _unitOfWork.ExperienceRepo.GetUserExperiencesWithIndustryAndCompany(userid);

			var list = _mapper.Map<List<ExperienceVM>>(experienceList);
			return list;
		}
		public async Task<ExperienceVM> GetExperience(int id)
		{
			var exp = await _unitOfWork.ExperienceRepo.Get(e => e.Id == id, includeProperties: "Industry,Company");
			var experience = _mapper.Map<ExperienceVM>(exp);
			return experience;
		}
		public async Task<List<CompanyVM>> GetAllCompanies()
		{
			var companies = await _unitOfWork.CompanyRepo.GetAll();
			var companyVMs = _mapper.Map<List<CompanyVM>>(companies);

			return companyVMs;
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
			var company = await _unitOfWork.CompanyRepo.Get(val => val.ComanyName == experience.Company.ComanyName);
			if (company == null)
			{
				return false;
			}
			mappedExperience.IndustryId = industry.Id;
			mappedExperience.CompanyId = company.Id;

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

			var company = await _unitOfWork.CompanyRepo.Get(val => val.ComanyName == experience.Company.ComanyName);
			if (company == null)
			{
				return false;
			}
			existingExperience.IndustryId = industry.Id;
			existingExperience.CompanyId = company.Id;

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

		public async Task<string> GetUserUrl(string userId)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userId);
			return user.ProfileUrl;
		}
		public async Task<bool> UpdateUserUrl(string url, string userId)
		{
			if ( await IsUniqueUrl(url))
			{
				var user = await _unitOfWork.UserRepo.Get(u => u.Id == userId);
				user.ProfileUrl = url;
				_unitOfWork.UserRepo.Update(user);
				await _unitOfWork.SaveAsync();
				return true;
			}
			return false;
		}
		private async Task<bool> IsUniqueUrl(string url)
		{
			var existingUrls = await _unitOfWork.UserRepo.GetAll(u => u.ProfileUrl == url);
			if (existingUrls.Any())
			{
				return false;
			}
			return true;
		}



		public async Task<List<PositionVM>> GetAllPositions()
		{
			var positions = await _unitOfWork.PositionRepo.GetAll();
			var positionVMs = _mapper.Map<List<PositionVM>>(positions);

			return positionVMs;
		}


		public async Task<bool> AddOpenToWork(OpenToWorkVM openToWorkVM, string userId)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userId);
			if (user.OpenToWork)
			{
				throw new Exception("User is already open to work.");
			}
			user.OpenToWork = true;

			var openToWork = _mapper.Map<OpenToWork>(openToWorkVM);
			openToWork.ApplicationUserId = userId;

			await _unitOfWork.OpenToWorkRepo.Add(openToWork);
			_unitOfWork.UserRepo.Update(user);
			await _unitOfWork.SaveAsync();

			foreach (var positionVM in openToWorkVM.OpenToWorkPositions)
			{
				var newPosition = new OpenToWorkPosition
				{
					OpenToWorkId = openToWork.Id,
					PositionId = positionVM.PositionId
				};
				var existingPosition = await _unitOfWork.OpenToWorkPositionRepo.Get(op => op.OpenToWorkId == newPosition.OpenToWorkId && op.PositionId == newPosition.PositionId);
				if (existingPosition == null)
				{
					await _unitOfWork.OpenToWorkPositionRepo.Add(newPosition);
				}
			}

			foreach (var cityVM in openToWorkVM.OpenToWorkCities)
			{
				var newCity = new OpenToWorkCity
				{
					OpenToWorkId = openToWork.Id,
					CityId = cityVM.CityId
				};

				var existingCity = await _unitOfWork.OpenToWorkCityRepo.Get(oc => oc.OpenToWorkId == newCity.OpenToWorkId && oc.CityId == newCity.CityId);
				if (existingCity == null)
				{
					await _unitOfWork.OpenToWorkCityRepo.Add(newCity);
				}
			}

			foreach (var countryVM in openToWorkVM.OpenToWorkCountries)
			{
				var newCountry = new OpenToWorkCountry
				{
					OpenToWorkId = openToWork.Id,
					CountryId = countryVM.CountryId
				};

				var existingCountry = await _unitOfWork.OpenToWorkCountryRepo.Get(oc => oc.OpenToWorkId == newCountry.OpenToWorkId && oc.CountryId == newCountry.CountryId);
				if (existingCountry == null)
				{
					await _unitOfWork.OpenToWorkCountryRepo.Add(newCountry);
				}
			}

			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateOpenToWork(OpenToWorkVM openToWorkVM, string userId)
		{
			var existingOpenToWork = await _unitOfWork.OpenToWorkRepo.Get(o => o.ApplicationUserId == userId,includeProperties: "OpenToWorkCities,OpenToWorkCountries,OpenToWorkPositions");
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
					OpenToWorkId = existingOpenToWork.Id,
					PositionId = positionVM.PositionId
				});
			}

			_unitOfWork.OpenToWorkCityRepo.RemoveRange(existingOpenToWork.OpenToWorkCities);

			foreach (var cityVM in openToWorkVM.OpenToWorkCities)
			{
				existingOpenToWork.OpenToWorkCities.Add(new OpenToWorkCity
				{
					OpenToWorkId = existingOpenToWork.Id,
					CityId = cityVM.CityId
				});
			}

			_unitOfWork.OpenToWorkCountryRepo.RemoveRange(existingOpenToWork.OpenToWorkCountries);

			foreach (var countryVM in openToWorkVM.OpenToWorkCountries)
			{
				existingOpenToWork.OpenToWorkCountries.Add(new OpenToWorkCountry
				{
					OpenToWorkId = existingOpenToWork.Id,
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
			_unitOfWork.UserRepo.Update(user);

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


		public async Task<bool> AddServices(ServiceVM serviceVM, string userId)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userId);
			if (user.OpenToServices)
			{
				throw new Exception("User is already open to ervices.");
			}
			user.OpenToServices = true;

			var openToServices = _mapper.Map<Service>(serviceVM);
			openToServices.ApplicationUserId = userId;

			await _unitOfWork.ServiceRepo.Add(openToServices);
			_unitOfWork.UserRepo.Update(user);
			await _unitOfWork.SaveAsync();

			foreach (var positionVM in openToServices.ServicePositions)
			{
				var newPosition = new ServicePosition
				{
					ServiceId = openToServices.Id,
					PositionId = positionVM.PositionId
				};
				var existingPosition = await _unitOfWork.ServicePositionRepo.Get(op => op.ServiceId == newPosition.ServiceId && op.PositionId == newPosition.PositionId);
				if (existingPosition == null)
				{
					await _unitOfWork.ServicePositionRepo.Add(newPosition);
				}
			}

			foreach (var cityVM in openToServices.ServiceCities)
			{
				var newCity = new ServiceCity
				{
					ServiceId = openToServices.Id,
					CityId = cityVM.CityId
				};

				var existingCity = await _unitOfWork.ServiceCityRepo.Get(oc => oc.ServiceId == newCity.ServiceId && oc.CityId == newCity.CityId);
				if (existingCity == null)
				{
					await _unitOfWork.ServiceCityRepo.Add(newCity);
				}
			}

			foreach (var countryVM in openToServices.ServiceCountries)
			{
				var newCountry = new ServiceCountry
				{
					ServiceId = openToServices.Id,
					CountryId = countryVM.CountryId
				};

				var existingCountry = await _unitOfWork.ServiceCountryRepo.Get(oc => oc.ServiceId == newCountry.ServiceId && oc.CountryId == newCountry.CountryId);
				if (existingCountry == null)
				{
					await _unitOfWork.ServiceCountryRepo.Add(newCountry);
				}
			}

			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateServices(ServiceVM serviceVM, string userId)
		{
			var existingServices = await _unitOfWork.ServiceRepo.Get(o => o.ApplicationUserId == userId, includeProperties: "ServiceCities,ServiceCountries,ServicePositions");
			if (existingServices == null)
			{
				return false;
			}
			existingServices.GeneralInformation= serviceVM.GeneralInformation;
			existingServices.Currency = serviceVM.Currency;
			existingServices.Salary = serviceVM.Salary;
			existingServices.IsRemoteOk = serviceVM.IsRemoteOk;

			_unitOfWork.ServicePositionRepo.RemoveRange(existingServices.ServicePositions);

			foreach (var positionVM in serviceVM.ServicePositions)
			{
				existingServices.ServicePositions.Add(new ServicePosition
				{
					ServiceId = existingServices.Id,
					PositionId = positionVM.PositionId
				});
			}

			_unitOfWork.ServiceCityRepo.RemoveRange(existingServices.ServiceCities);

			foreach (var cityVM in serviceVM.ServiceCities)
			{
				existingServices.ServiceCities.Add(new ServiceCity
				{
					ServiceId = existingServices.Id,
					CityId = cityVM.CityId
				});
			}

			_unitOfWork.ServiceCountryRepo.RemoveRange(existingServices.ServiceCountries);

			foreach (var countryVM in serviceVM.ServiceCountries)
			{
				existingServices.ServiceCountries.Add(new ServiceCountry
				{
					ServiceId = existingServices.Id,
					CountryId = countryVM.CountryId
				});
			}

			_unitOfWork.ServiceRepo.Update(existingServices);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> DeleteServices(string userId)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userId);
			user.OpenToServices = false;
			_unitOfWork.UserRepo.Update(user);

			var existingServices = await _unitOfWork.ServiceRepo.Get(o => o.ApplicationUserId == userId, includeProperties: "ServiceCities,ServiceCountries,ServicePositions");
			if (existingServices == null)
			{
				return false;
			}

			_unitOfWork.ServiceCityRepo.RemoveRange(existingServices.ServiceCities);
			_unitOfWork.ServiceCountryRepo.RemoveRange(existingServices.ServiceCountries);
			_unitOfWork.ServicePositionRepo.RemoveRange(existingServices.ServicePositions);
			_unitOfWork.ServiceRepo.Remove(existingServices);

			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<ServiceVM> GetServicesByUserId(string userId)
		{
			var services = await _unitOfWork.ServiceRepo.Get(
				o => o.ApplicationUserId == userId,
				includeProperties: "ServiceCities,ServiceCountries,ServicePositions"
			);
			var servicesVM = _mapper.Map<ServiceVM>(services);
			return servicesVM;
		}
	}

}
