using BLL.DTOs;
using System.ComponentModel.DataAnnotations;


namespace BLL.DTOs
{
    public class ExperienceDTO
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string CompanyName  { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		[Required]
		public string Description { get; set; }
		public string? ProfileHeadline { get; set; }

		public string ApplicationUserId { get; set; }

		public virtual UserDTO ApplicationUser { get; set; }

		public int IndustryId { get; set; }
		public virtual IndustryDTO Industry { get; set; }

	}
}
