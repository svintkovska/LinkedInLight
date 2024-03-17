using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class PostVM
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public string Image { get; set; }
		public DateTime DatePosted { get; set; }
		public bool IsBanned { get; set; }
		public ICollection<CommentVM> Comments { get; set; }
		public ICollection<LikeVM> Likes { get; set; }
	}
}
