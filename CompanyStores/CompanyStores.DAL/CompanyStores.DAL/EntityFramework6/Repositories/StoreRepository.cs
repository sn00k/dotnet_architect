using CompanyStores.DAL.EntityFramework6.Infrastructure;
using CompanyStores.DAL.Interfaces.Repositories;
using CompanyStores.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.DAL.EntityFramework6.Repositories
{
	public class StoreRepository : BaseRepository<Store, Guid>, IStoreRepository
	{
		public StoreRepository(CompanyStoresContext context) : base(context)
		{
			
		}

		public async Task<IList<Store>> GetByNameAsync(string name)
		{
			return await DbSet.Where(x => x.Name.Contains(name)).ToListAsync();
		}

		public async Task<IEnumerable<Store>> GetByCompanyIdAsync(Guid id)
		{
			return await DbSet.Where(x => x.CompanyId == id).ToListAsync();
		}
	}
}
