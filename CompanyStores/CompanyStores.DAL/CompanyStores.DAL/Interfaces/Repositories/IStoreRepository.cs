using CompanyStores.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.DAL.Interfaces.Repositories
{
	public interface IStoreRepository : IRepository<Store, Guid>
	{
		Task<IList<Store>> GetByNameAsync(string name);
		Task<IEnumerable<Store>> GetByCompanyIdAsync(Guid id);
	}
}
