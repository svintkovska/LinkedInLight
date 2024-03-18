using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class PhoneNumber
	{
		[Key]
		public int Id { get; set; }
		public string Number { get; set; }
		public string Type { get; set; }
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
	}
}
