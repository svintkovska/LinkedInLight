using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class CourseVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Number { get; set; }
		public string AssociatedWith { get; set; }
		public string ApplicationUserId { get; set; }
	}
}
