using DLL.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
	public class JobPostingDTO
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime PostedAt { get; set; }

		public int CompanyId { get; set; }
		public virtual CompanyDTO Company { get; set; }

		public string RecruiterId { get; set; }
		public virtual UserDTO Recruiter { get; set; }

		public string Location { get; set; }
		public string EmploymentType { get; set; }
		public string ExperienceLevel { get; set; }
		public string Function { get; set; }
		public int IndustryId { get; set; }
		public IndustryDTO Industry { get; set; }

		public virtual ICollection<JobApplicationDTO> Applications { get; set; }
		public virtual ICollection<ScreeningQuestionDTO> ScreeningQuestions { get; set; }

	}
}
