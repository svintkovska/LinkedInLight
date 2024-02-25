using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
	public class MessageDTO
	{
		[Key]
		public int Id { get; set; }

		public string Content { get; set; }

		public DateTime SentAt { get; set; }

		public bool IsRead { get; set; }

		public string SenderId { get; set; }
		public virtual UserDTO Sender { get; set; }

		public string ReceiverId { get; set; }
		public virtual UserDTO Receiver { get; set; }
	}
}
