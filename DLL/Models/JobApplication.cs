using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
	public class JobApplication
	{
		[Key]
		public int Id { get; set; }
		public DateTime AppliedAt { get; set; }

		public int JobPostingId { get; set; }
		public virtual JobPosting JobPosting { get; set; }

		public string CandidateId { get; set; }
		public virtual ApplicationUser Candidate { get; set; }

		public string Status { get; set; } // Pending, Accepted, Rejected, etc.
	}
}
