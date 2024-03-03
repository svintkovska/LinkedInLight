using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class JobPosting
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime PostedAt { get; set; }

		public int CompanyId { get; set; }
		public virtual Company Company { get; set; }

		public string RecruiterId { get; set; }
		public virtual ApplicationUser Recruiter { get; set; }

		public string Location { get; set; }
		public string EmploymentType { get; set; }
		public string ExperienceLevel { get; set; }
		public string Function { get; set; }
		public int IndustryId { get; set; }
		public Industry Industry { get; set; }

		public virtual ICollection<JobApplication> Applications { get; set; }
		public virtual ICollection<ScreeningQuestion> ScreeningQuestions { get; set; }

	}
}
