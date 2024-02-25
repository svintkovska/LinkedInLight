using BLL.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class CompanyDTO
	{
		public int Id { get; set; }
		[Required]
		public string ComanyName { get; set; }
		[Required]
		public string LinkedinUrl { get; set; }
		public string? WebsiteUrl { get; set; }
		
		[Required]
		public string OrganizationSize { get; set; }

		[Required]
		public string OrganizationType { get; set; }

		public string? LogoImg { get; set; }

		public string ApplicationUserId { get; set; }

		public virtual UserDTO ApplicationUser { get; set; }

		public int IndustryId { get; set; }
		public virtual IndustryDTO Industry { get; set; }

	}
}
