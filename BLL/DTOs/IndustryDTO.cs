
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
	public class IndustryDTO
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Namme { get; set; }
		
	}
}
