using BLL.DTOs;
using System.ComponentModel.DataAnnotations;


namespace BLL.DTOs
{
    public class EducationDTO
	{
		public int Id { get; set; }
		[Required]
		public string School { get; set; }
		[Required]
		public string Degree { get; set; }
		[Required]
		public string FieldOfStudy{ get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		[Required]
		public string? Grade { get; set; }
		public string? Description { get; set; }

		public string ApplicationUserId { get; set; }

		public virtual UserDTO ApplicationUser { get; set; }


	}
}
