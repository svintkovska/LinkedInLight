using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class Like
	{
		[Key]
		public int Id { get; set; }
		public int? PostId { get; set; }
		public virtual Post Post { get; set; }
		public int? CommentId { get; set; }
		public virtual Comment Comment { get; set; }
		public string ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}
