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
	public class Industry
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Namme { get; set; }
		
	}
}
