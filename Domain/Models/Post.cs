using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Post
	{
		[Key]
		public int Id { get; set; }
		public string Content { get; set; }
		public string? Image { get; set; }
		public DateTime DatePosted { get; set; }
		public bool IsBanned { get; set; }

		public string ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }
		public virtual ICollection<Like> Likes { get; set; }
	}
}
