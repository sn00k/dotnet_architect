using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.BLL.Services.Exceptions
{
    public class StoreNotFoundException : Exception
    {
		public StoreNotFoundException(string message) : base(message)
		{

		}
    }
}
