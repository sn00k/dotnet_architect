using CompanyStores.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyStores.DAL.Models;
using CompanyStores.DAL.EntityFramework6.Infrastructure;
using System.Data.Entity;

namespace CompanyStores.DAL.EntityFramework6.Repositories
{
	public class CompanyRepository : BaseRepository<Company, Guid>, ICompanyRepository
	{
		public CompanyRepository(CompanyStoresContext context) : base(context)
		{

		}

		public async Task<IList<Company>> GetByNameAsync(string name)
		{
			return await DbSet.Where(x => x.Name.Contains(name)).ToListAsync();
		}
	}
}
