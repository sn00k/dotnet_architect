using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.BLL.Services.Exceptions
{
    public class MissingCompanyIdException : Exception
    {
		public MissingCompanyIdException(string message) : base(message)
		{

		}
    }
}
