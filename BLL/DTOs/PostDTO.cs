using DLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Data
{
    public class PostDTO
	{
		[Key]
		public int Id { get; set; }
		public string Content { get; set; }
		public string? Image { get; set; }
		public DateTime DatePosted { get; set; }

		public string ApplicationUserId { get; set; }
		public virtual UserDTO ApplicationUser { get; set; }

		public virtual ICollection<CommentDTO> Comments { get; set; }
		public virtual ICollection<LikeDTO> Likes { get; set; }
	}
}
