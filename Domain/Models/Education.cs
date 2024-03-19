using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Education
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string School { get; set; }
		[Required]
		public string Degree { get; set; }
		[Required]
		public string FieldOfStudy{ get; set; }
		[Required]
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool CurrentlyStudying { get; set; }

		[Required]
		public string? Grade { get; set; }
		public string? Description { get; set; }

		public string ApplicationUserId { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; }


	}
}
