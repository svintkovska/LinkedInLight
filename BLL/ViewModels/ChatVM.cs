using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class ChatVM
	{
		public int Id { get; set; }

		public string Participant1Id { get; set; }
		public string Participant2Id { get; set; }

		public int LastMessageId { get; set; }
		public DateTime LastMessageSentAt { get; set; }

		public bool IsRead { get; set; }
		public bool IsDeletedForParticipant1 { get; set; }
		public bool IsDeletedForParticipant2 { get; set; }
		public ICollection<MessageVM> Messages { get; set; }
	}
}
