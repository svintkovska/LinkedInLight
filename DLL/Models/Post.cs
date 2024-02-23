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
    public class Post
	{
		[Key]
		public int Id { get; set; }
		public string? Text { get; set; }
		public string? Image { get; set; }
		
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }

	}
}
