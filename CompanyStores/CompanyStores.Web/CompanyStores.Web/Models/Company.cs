using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.Web.Models
{
    public class Company
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public int OrganizationNumber { get; set; }

		public Store Store { get; set; }
	}
}
