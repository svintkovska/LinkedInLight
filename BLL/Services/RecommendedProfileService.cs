using AutoMapper;
using BLL.Interfaces;
using BLL.Utilities.SignalR;
using BLL.ViewModels;
using DLL.Constants;
using DLL.Repositories.IRepository;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class RecommendedProfileService: ProfileService, IRecommendedProfileService
	{
		private readonly IHubContext<ChatHub> _hubContext;
		private readonly IChatService _chatService;
		private readonly IConfiguration _configuration;

		public RecommendedProfileService(IUploadService uploadService, IUnitOfWork unitOfWork, IMapper mapper, IHubContext<ChatHub> hubContext, IChatService chatService, IConfiguration configuration )
			: base(uploadService, unitOfWork, mapper)
		{
			_hubContext = hubContext;
			_chatService = chatService;
			_configuration = configuration;
		}

		public async Task<List<CertificationVM>> GetUserCertifications(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Certifications");
			var certificationList = user.Certifications.ToList();
			var list = _mapper.Map<List<CertificationVM>>(certificationList);
			return list;
		}
		public async Task<CertificationVM> GetCertification(int id)
		{
			var crt = await _unitOfWork.CertificationRepo.Get(e => e.Id == id);
			var certification = _mapper.Map<CertificationVM>(crt);
			return certification;
		}
		public async Task<bool> AddCertification(CertificationVM certification, string userId)
		{
			var mappedCertification = _mapper.Map<Certification>(certification);
			mappedCertification.ApplicationUserId = userId;
			await _unitOfWork.CertificationRepo.Add(mappedCertification);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveCertification(int certificationId)
		{
			var certification = await _unitOfWork.CertificationRepo.Get(e => e.Id == certificationId);
			_unitOfWork.CertificationRepo.Remove(certification);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateCertification(CertificationVM certification)
		{
			var existingCertification = await _unitOfWork.CertificationRepo.Get(e => e.Id == certification.Id);
			if (existingCertification == null)
			{
				return false;
			}

			existingCertification.Name = certification.Name;
			existingCertification.IssuingOrganization = certification.IssuingOrganization;
			existingCertification.IssueDate = certification.IssueDate;
			existingCertification.ExpirationDate = certification.ExpirationDate;
			existingCertification.CredentialURL = certification.CredentialURL;
			existingCertification.CredentialId = certification.CredentialId;

			_unitOfWork.CertificationRepo.Update(existingCertification);
			await _unitOfWork.SaveAsync();
			return true;
		}



		public async Task<List<CourseVM>> GetUserCourses(string userid)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userid, includeProperties: "Courses");
			var coursesList = user.Courses.ToList();
			var list = _mapper.Map<List<CourseVM>>(coursesList);
			return list;
		}
		public async Task<CourseVM> GetCourse(int id)
		{
			var crs = await _unitOfWork.CourseRepo.Get(e => e.Id == id);
			var course = _mapper.Map<CourseVM>(crs);
			return course;
		}
		public async Task<bool> AddCourse(CourseVM course, string userId)
		{
			var mappedCourse = _mapper.Map<Course>(course);
			mappedCourse.ApplicationUserId = userId;
			await _unitOfWork.CourseRepo.Add(mappedCourse);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> RemoveCourse(int courseId)
		{
			var course = await _unitOfWork.CourseRepo.Get(e => e.Id == courseId);
			_unitOfWork.CourseRepo.Remove(course);
			await _unitOfWork.SaveAsync();
			return true;
		}
		public async Task<bool> UpdateCourse(CourseVM course)
		{
			var existingCourse = await _unitOfWork.CourseRepo.Get(e => e.Id == course.Id);
			if (existingCourse == null)
			{
				return false;
			}

			existingCourse.Name = course.Name;
			existingCourse.Number = course.Number;
			existingCourse.AssociatedWith = course.AssociatedWith;


			_unitOfWork.CourseRepo.Update(existingCourse);
			await _unitOfWork.SaveAsync();
			return true;
		}



		public async Task<List<ProjectVM>> GetUserProjects(string userid)
		{
			var projects = await _unitOfWork.ProjectRepo.GetUserProjects(userid);
			var list = _mapper.Map<List<ProjectVM>>(projects);
			return list;
		}
		public async Task<ProjectVM> GetProject(int id)
		{
			var proj = await _unitOfWork.ProjectRepo.GetProjectWithContributors(id);
			var project = _mapper.Map<ProjectVM>(proj);
			return project;
		}
		public async Task<bool> AddProjectWithContributors(ProjectVM projectVM, string userId)
		{
			var project = _mapper.Map<Project>(projectVM);
			project.ApplicationUserId = userId;
			await _unitOfWork.ProjectRepo.Add(project);
			await _unitOfWork.SaveAsync();

			foreach (var contributorId in projectVM.ProjectContributors.Select(pc => pc.ApplicationUserId))
			{
				var contributor = await _unitOfWork.UserRepo.Get(u => u.Id == contributorId);
				if (contributor != null)
				{
					var projectContributor = new ProjectContributor
					{
						ProjectId = project.Id,
						ApplicationUserId = contributor.Id
					};
					await _unitOfWork.ProjectContributorRepo.Add(projectContributor);
				}
			}
			await _unitOfWork.SaveAsync();

			return true;
		}
		public async Task<bool> RemoveProject(int projectId)
		{
			var project = await _unitOfWork.ProjectRepo.GetProjectWithContributors(projectId);

			if (project == null)
			{
				return false;
			}

			foreach (var contributor in project.ProjectContributors.ToList())
			{
				_unitOfWork.ProjectContributorRepo.Remove(contributor);
			}
			_unitOfWork.ProjectRepo.Remove(project);

			await _unitOfWork.SaveAsync();

			return true;
		}
		public async Task<bool> UpdateProjectWithContributors(ProjectVM projectVM)
		{
			var project = await _unitOfWork.ProjectRepo.Get(p => p.Id == projectVM.Id, includeProperties: "ProjectContributors");

			if (project == null)
			{
				return false;
			}

			_mapper.Map(projectVM, project);

			var contributorsToRemove = project.ProjectContributors.Where(pc => !projectVM.ProjectContributors.Any(pcv => pcv.ApplicationUserId == pc.ApplicationUserId)).ToList();
			foreach (var contributor in contributorsToRemove)
			{
				_unitOfWork.ProjectContributorRepo.Remove(contributor);
			}

			foreach (var contributorId in projectVM.ProjectContributors.Select(pc => pc.ApplicationUserId))
			{
				if (!project.ProjectContributors.Any(pc => pc.ApplicationUserId == contributorId))
				{
					var newContributor = await _unitOfWork.UserRepo.Get(u => u.Id == contributorId);
					if (newContributor != null)
					{
						var projectContributor = new ProjectContributor
						{
							ProjectId = project.Id,
							ApplicationUserId = newContributor.Id
						};
						await _unitOfWork.ProjectContributorRepo.Add(projectContributor);
					}
				}
			}
			await _unitOfWork.SaveAsync();

			return true;
		}


		public async Task<RecommendationRequestVM> GETRequestRecommendation(string userId)
		{
			var user = await _unitOfWork.UserRepo.Get(u => u.Id == userId, includeProperties: "Connections,Experiences");

			var connections = user.Connections.Select(c => new UserVM
			{
				Id = c.ConnectedUserId, 
				FirstName = c.ConnectedUser.FirstName,
				LastName = c.ConnectedUser.LastName, 
				AdditionalName = c.ConnectedUser.AdditionalName, 
				Image = c.ConnectedUser.Image, 
				LastPosition = _unitOfWork.ExperienceRepo.GetUserLastPosition(c.ConnectedUserId) 
			}).ToList();

			var request = new RecommendationRequestVM
			{
				Connections = connections,
				RequestMessage = "Hi, would you write me a recommendation please?"
			};

			return request;
		}
		public async Task<List<RecommendationVM>> GetReceivedRecommendations(string userId)
		{
			var receivedRecommendations = await _unitOfWork.RecommendationRepo.GetAll(r => r.ReceiverId == userId && r.Status == RecommendationStatus.GIVEN, includeProperties:"ApplicationUsers");
			var list = _mapper.Map<List<RecommendationVM>>(receivedRecommendations);
			return list;
		}
		public async Task<List<RecommendationVM>> GetGivenRecommendations(string userId)
		{
			var givenRecommendations = await _unitOfWork.RecommendationRepo.GetAll(r => r.SenderId == userId && r.Status == RecommendationStatus.GIVEN, includeProperties: "ApplicationUsers");
			var list = _mapper.Map<List<RecommendationVM>>(givenRecommendations);
			return list;
		}
		public async Task<List<RecommendationVM>> GetPendingRecommendations(string userId)
		{
			var pendingRecommendations = await _unitOfWork.RecommendationRepo.GetAll(r => r.SenderId == userId && r.Status == RecommendationStatus.PENDING, includeProperties: "ApplicationUsers");
			var list = _mapper.Map<List<RecommendationVM>>(pendingRecommendations);
			return list;
		}
		public async Task<bool> RequestRecommendation(RecommendationVM recommendationRequest)
		{
			var existingRequest = await _unitOfWork.RecommendationRepo.Get(r => r.SenderId == recommendationRequest.SenderId && r.ReceiverId == recommendationRequest.ReceiverId && r.Status == RecommendationStatus.PENDING);

			if (existingRequest != null)
			{
				return false; 
			}

			var recommendation = new Recommendation
			{
				SenderId = recommendationRequest.SenderId,
				ReceiverId = recommendationRequest.ReceiverId,
				Status = RecommendationStatus.PENDING,
				DateRequested = DateTime.UtcNow,
				Relationship = recommendationRequest.Relationship,
				PositionAtTheTime = recommendationRequest.PositionAtTheTime
				
			};

			_unitOfWork.RecommendationRepo.Add(recommendation);
			await _unitOfWork.SaveAsync();

			var sender = await _unitOfWork.UserRepo.Get(u => u.Id == recommendationRequest.SenderId);
			var frontendURL = _configuration.GetValue<string>("FrontEndURL");
			var callbackURL = $"{frontendURL}/giveRecommendation/{recommendationRequest.SenderId}";

			var message = new MessageVM { 
				ReceiverId = recommendationRequest.ReceiverId,
				SenderId = recommendation.SenderId,
				Content = recommendationRequest.RequestMessage + $"\\r\\n\\r\\nWrite {sender.FirstName} a recommendation:\\r\\n {callbackURL}"
			};
			await _chatService.SendMessage(message);

			return true;
		}
		public async Task<bool> GiveRecommendation(RecommendationVM recommendation)
		{
			var requestedRecommendation = await _unitOfWork.RecommendationRepo.Get(r => r.Id == recommendation.Id && r.Status == RecommendationStatus.PENDING);

			if (requestedRecommendation == null)
			{
				return false; 
			}

			requestedRecommendation.Content = recommendation.Content;
			requestedRecommendation.Status = RecommendationStatus.GIVEN;
			requestedRecommendation.DateGiven = DateTime.UtcNow;

			_unitOfWork.RecommendationRepo.Update(requestedRecommendation);
			await _unitOfWork.SaveAsync();

			return true;
		}

	}
}
