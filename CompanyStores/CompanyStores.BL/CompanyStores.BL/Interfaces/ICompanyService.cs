using CompanyStores.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.BLL.Interfaces
{
    public interface ICompanyService
    {
		Task<IList<Company>> GetByNameAsync(string name);
		Task<IList<Company>> GetAllAsync();

		Task<Company> GetByIdAsync(Guid id);

		Task SaveEditedCompanyAsync(Company company);
		Task CreateCompanyAsync(Company company);
		Task DeleteCompanyAsync(Company company);
	}
}
