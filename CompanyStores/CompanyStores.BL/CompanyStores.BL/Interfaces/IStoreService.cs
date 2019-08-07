using CompanyStores.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.BLL.Interfaces
{
    public interface IStoreService
    {
		Task<IList<Store>> GetByNameAsync(string name);
		Task<IList<Store>> GetAllAsync();

		Task<Store> GetByIdAsync(Guid id);
		Task<IEnumerable<Store>> GetByCompanyIdAsync(Guid id);

		Task SaveEditedStoreAsync(Store store);
		Task CreateStoreAsync(Store store);
		Task DeleteStoreAsync(Store store);
	}
}
