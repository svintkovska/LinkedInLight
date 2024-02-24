using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
	public class ConnectionRequest
	{
		[Key]
		public int Id { get; set; }

		public string SenderId { get; set; }
		public virtual ApplicationUser Sender { get; set; }

		public string ReceiverId { get; set; }
		public virtual ApplicationUser Receiver { get; set; }

		public string Status { get; set; } // Pending, Accepted, Rejected
	}
}
