using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class CertificationVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string IssuingOrganization { get; set; }
		public DateTime IssueDate { get; set; }
		public DateTime ExpirationDate { get; set; }
		public string? CredentialId { get; set; }
		public string? CredentialURL { get; set; }
	}
}
