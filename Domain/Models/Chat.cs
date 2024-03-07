using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class Chat
	{
		[Key]
		public int Id { get; set; }

		public string Participant1Id { get; set; }
		public string Participant2Id { get; set; }

		public int? LastMessageId { get; set; }
		public DateTime? LastMessageSentAt { get; set; }

		public bool IsRead { get; set; }
		public bool IsDeletedForParticipant1 { get; set; }
		public bool IsDeletedForParticipant2 { get; set; }
		public virtual ICollection<Message> Messages { get; set; }
	}
}
