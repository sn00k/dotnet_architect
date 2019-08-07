using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.Web.Models
{
    public class Store
    {
		public Guid Id { get; set; }
		public Guid CompanyId { get; set; }

		public virtual Company Company { get; set; }

		public string Name { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string Zip { get; set; }
		public string Country { get; set; }

		public string Longitude { get; set; }
		public string Latitude { get; set; }
	}
}
