using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class MessageVM
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public string AttachedFileName { get; set; }
		public DateTime SentAt { get; set; }
		public bool IsRead { get; set; }
		public bool IsDeletedForSender { get; set; }
		public bool IsDeletedForReceiver { get; set; }
		public string SenderId { get; set; }
		public string ReceiverId { get; set; }
		public int ChatId { get; set; }
	}
}
