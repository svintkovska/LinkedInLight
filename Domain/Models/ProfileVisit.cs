using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class ProfileVisit
	{
		[Key]
		public int Id { get; set; }
		public string VisitorId { get; set; }  
		public string ProfileOwnerId { get; set; }  
		public DateTime VisitDateTime { get; set; }

		[ForeignKey("VisitorId")]
		public virtual ApplicationUser Visitor { get; set; }

		[ForeignKey("ProfileOwnerId")]
		public virtual ApplicationUser ProfileOwner { get; set; }

	}
}
