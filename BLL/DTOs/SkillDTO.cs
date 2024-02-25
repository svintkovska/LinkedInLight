using BLL.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
	public class SkillDTO
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string ApplicationUserId { get; set; }
		public virtual UserDTO ApplicationUser { get; set; }


	}
}
