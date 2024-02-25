using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
	public class JobApplicationDTO
	{
		[Key]
		public int Id { get; set; }
		public DateTime AppliedAt { get; set; }

		public int JobPostingId { get; set; }
		public virtual JobPostingDTO JobPosting { get; set; }

		public string CandidateId { get; set; }
		public virtual UserDTO Candidate { get; set; }

		public string Status { get; set; } // Pending, Accepted, Rejected, etc.
	}
}
