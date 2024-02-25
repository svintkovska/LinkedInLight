using System.ComponentModel.DataAnnotations;


namespace BLL.DTOs
{
	public class ScreeningQuestionDTO
	{
		[Key]
		public int Id { get; set; }
		public string Question { get; set; }
		public int JobPostingId { get; set; }
		public virtual JobPostingDTO JobPosting { get; set; }
	}
}
