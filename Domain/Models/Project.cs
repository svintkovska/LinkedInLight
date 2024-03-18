using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class Project
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool CurrentlyWorking { get; set; }
		public string AssociatedWith { get; set; }
		public virtual ICollection<ProjectContributor> ProjectContributors { get; set; }

	}
}
