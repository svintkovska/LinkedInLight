using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public string? AdditionalName { get; set; }
		public string? Headline { get; set; }
		public string? CurrentPosition { get; set; }
		public string? Industry { get; set; }
		[Required]
		public string Country { get; set; }
		[Required]
		public string City { get; set; }

	}
}
