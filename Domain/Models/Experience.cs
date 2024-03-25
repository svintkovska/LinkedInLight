using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Experience
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public int CompanyId  { get; set; }
		public virtual Company Company { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool CurrentlyWorking { get; set; }

		[Required]
		public string? Description { get; set; }
		public string? ProfileHeadline { get; set; }

		public string ApplicationUserId { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }

		public int IndustryId { get; set; }
		public virtual Industry Industry { get; set; }

	}
}
