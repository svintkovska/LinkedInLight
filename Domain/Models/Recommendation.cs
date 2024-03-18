using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class Recommendation
	{
		[Key]
		public int Id { get; set; }
		public string PositionAtTheTime { get; set; }
		public string Relationship { get; set; }
		public string Content { get; set; }
		public string GivenByUserId { get; set; }
		public ApplicationUser GivenByUser { get; set; }
		public string ReceivedByUserId { get; set; }
		public ApplicationUser ReceivedByUser { get; set; }
		public DateTime GivenAt { get; set; }
	}
}
