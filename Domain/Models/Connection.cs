using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class Connection
	{
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		public virtual ApplicationUser User { get; set; }

		public string ConnectedUserId { get; set; }
		public virtual ApplicationUser ConnectedUser { get; set; }
	}
}
