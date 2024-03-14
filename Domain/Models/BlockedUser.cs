using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class BlockedUser
	{
		[Key]
		public int Id { get; set; }

		public string UserId { get; set; }

		[ForeignKey("UserId")]
		public virtual ApplicationUser User { get; set; }

		public string BlockedUserId { get; set; }

		[ForeignKey("BlockedUserId")]
		public virtual ApplicationUser BlockedAppUser { get; set; }
	}
}
