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
		public string GivenByUserId { get; set; }
		public string ReceivedByUserId { get; set; }
		public DateTime GivenAt { get; set; }
	}
}
