using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class CityVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int CountryId { get; set; }
	}
}
