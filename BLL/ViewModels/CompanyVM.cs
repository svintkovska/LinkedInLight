using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class CompanyVM
	{
		public int Id { get; set; }
		public string ComanyName { get; set; }
		public string LinkedinUrl { get; set; }
		public string? WebsiteUrl { get; set; }	
		public string OrganizationSize { get; set; }
		public string OrganizationType { get; set; }
		public string? LogoImg { get; set; }
		public string ApplicationUserId { get; set; }
		public int IndustryId { get; set; }

	}
}
