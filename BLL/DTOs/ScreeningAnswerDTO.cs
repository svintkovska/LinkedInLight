using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
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
