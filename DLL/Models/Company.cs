using DLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Data
{
    public class Company
	{
		[Key]
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
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }

		public int IndustryId { get; set; }
		[ForeignKey("IndustryId")]
		public Industry Industry { get; set; }

	}
}
