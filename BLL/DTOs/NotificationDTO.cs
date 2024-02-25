using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
	public class NotificationDTO
	{
		[Key]
		public int Id { get; set; }
		public string Content { get; set; }
		public bool IsRead { get; set; }
		public DateTime CreatedAt { get; set; }

		public string ApplicationUserId { get; set; }
		public virtual UserDTO ApplicationUser { get; set; }
	}
}
