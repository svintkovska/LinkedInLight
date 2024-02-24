using DLL.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
	public class Comment
	{
		[Key]
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
		public int PostId { get; set; }
		public virtual Post Post { get; set; }
		public string ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
		public virtual ICollection<Like> Likes { get; set; }
	}
}
