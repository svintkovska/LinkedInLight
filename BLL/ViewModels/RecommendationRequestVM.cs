using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class RecommendationRequestVM
	{
		public List<UserVM> Connections { get; set; }
		public string RequestMessage { get; set; }
	}
}
