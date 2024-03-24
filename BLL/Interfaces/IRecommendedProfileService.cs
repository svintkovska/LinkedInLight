﻿using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IRecommendedProfileService : IProfileService
	{
		public Task<List<CertificationVM>> GetUserCertifications(string userid);
		public Task<CertificationVM> GetCertification(int id);
		public Task<bool> AddCertification(CertificationVM certification, string userId);
		public Task<bool> RemoveCertification(int certificationId);
		public Task<bool> UpdateCertification(CertificationVM certification);


		public Task<List<CourseVM>> GetUserCourses(string userid);
		public Task<CourseVM> GetCourse(int id);
		public Task<bool> AddCourse(CourseVM course, string userId);
		public Task<bool> RemoveCourse(int courseId);
		public Task<bool> UpdateCourse(CourseVM course);


		public Task<List<ProjectVM>> GetUserProjects(string userid);
		public Task<ProjectVM> GetProject(int id);
		public Task<bool> AddProjectWithContributors(ProjectVM projectVM, string userId);
		public Task<bool> RemoveProject(int projectId);
		public Task<bool> UpdateProjectWithContributors(ProjectVM projectVM);
	}
}
