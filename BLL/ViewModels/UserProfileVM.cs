using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class UserProfileVM
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string AdditionalName { get; set; }
		public string Email { get; set; }
		public string Headline { get; set; }
		public string CurrentPosition { get; set; }
		public string Industry { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string About { get; set; }
		public string Image { get; set; }
		public string Background { get; set; }
		public bool IsBanned { get; set; }
		public bool OpenToWork { get; set; }
		public bool OpenToHire { get; set; }
		public string ProfileUrl { get; set; }
		public DateTime Birthday { get; set; }
		public bool IsRecruiter { get; set; }

		public ICollection<ExperienceVM> Experiences { get; set; }
		public  ICollection<EducationVM> Educations { get; set; }
		public  ICollection<SkillVM> Skills { get; set; }
		public  ICollection<LanguageVM> Languages { get; set; }
		public  ICollection<PostVM> Posts { get; set; }
		public virtual ICollection<CertificationVM> Certifications { get; set; }
		public virtual ICollection<ProjectVM> Projects { get; set; }
		public virtual ICollection<CourseVM> Courses { get; set; }
		public virtual ICollection<RecommendationVM> ReceivedRecommendations { get; set; }
		public virtual ICollection<RecommendationVM> GivenRecommendations { get; set; }
		public virtual ICollection<VolunteerExperienceVM> VolunteerExperiences { get; set; }
		public virtual ICollection<PhoneNumberVM> PhoneNumbers { get; set; }
		public virtual ICollection<WebsiteVM> Websites { get; set; }
	}
}
