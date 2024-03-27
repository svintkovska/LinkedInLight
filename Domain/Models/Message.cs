using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class Message
	{
		[Key]
		public int Id { get; set; }

		public string Content { get; set; }
		public string AttachedFileName { get; set; }
		public DateTime SentAt { get; set; }

		public bool IsRead { get; set; }
		public bool IsDeletedForSender { get; set; }
		public bool IsDeletedForReceiver { get; set; }
		public string SenderId { get; set; }
		public virtual ApplicationUser Sender { get; set; }

		public string ReceiverId { get; set; }
		public virtual ApplicationUser Receiver { get; set; }
		public int ChatId { get; set; } 
		public virtual Chat Chat { get; set; } 

	}
}
