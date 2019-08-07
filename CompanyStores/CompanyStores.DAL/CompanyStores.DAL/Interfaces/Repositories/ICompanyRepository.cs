using CompanyStores.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.DAL.Interfaces.Repositories
{
	public interface ICompanyRepository : IRepository<Company, Guid>
	{
		Task<IList<Company>> GetByNameAsync(string name);
	}
}
