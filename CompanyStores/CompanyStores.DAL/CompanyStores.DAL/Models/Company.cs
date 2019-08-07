using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.DAL.Models
{
    public class Company
    {
		public Guid Id { get; set; }

		public virtual IList<Store> Stores { get; set; }

		public string Name { get; set; }

		public int OrganizationNumber { get; set; }

		public string Notes { get; set; }
	}
}
