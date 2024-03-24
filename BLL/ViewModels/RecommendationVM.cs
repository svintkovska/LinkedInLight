using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class RecommendationVM
	{
		public int Id { get; set; }
		public string PositionAtTheTime { get; set; }
		public string Relationship { get; set; }
		public string Content { get; set; }
		public string SenderId { get; set; }
		public string RequestMessage { get; set; }
		public UserVM Sender { get; set; }
		public string ReceiverId { get; set; }
		public UserVM Receiver { get; set; }
		public DateTime DateGiven { get; set; }
		public DateTime DateRequested { get; set; }
		public string Status { get; set; }  
	}
}
