using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
	public class ScreeningQuestion
	{
		[Key]
		public int Id { get; set; }
		public string Question { get; set; }
		public int JobPostingId { get; set; }
		public virtual JobPosting JobPosting { get; set; }
	}
}
