using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class ScreeningAnswer
	{
		[Key]
		public int Id { get; set; }
		public string Answer { get; set; }

		public int ScreeningQuestionId { get; set; }
		public virtual ScreeningQuestion ScreeningQuestion { get; set; }

		public string CandidateId { get; set; }
		public virtual ApplicationUser Candidate { get; set; }
	}
}
