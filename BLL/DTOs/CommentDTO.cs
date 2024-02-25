using DLL.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
	public class CommentDTO
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
		public int PostId { get; set; }
		public virtual PostDTO Post { get; set; }
		public string ApplicationUserId { get; set; }
		public virtual UserDTO ApplicationUser { get; set; }
		public virtual ICollection<LikeDTO> Likes { get; set; }
	}
}
