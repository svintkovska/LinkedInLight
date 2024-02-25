using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
	public class ScreeningAnswerDTO
	{
		[Key]
		public int Id { get; set; }
		public string Answer { get; set; }

		public int ScreeningQuestionId { get; set; }
		public virtual ScreeningQuestionDTO ScreeningQuestion { get; set; }

		public string CandidateId { get; set; }
		public virtual UserDTO Candidate { get; set; }
	}
}
