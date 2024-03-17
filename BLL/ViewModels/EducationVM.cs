using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class EducationVM
	{
		public int Id { get; set; }
		public string School { get; set; }
		public string Degree { get; set; }
		public string FieldOfStudy { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string Grade { get; set; }
		public string Description { get; set; }

	}
}
