using CompanyStores.DAL.EntityFramework6.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.DAL.EntityFramework6.Repositories
{
	// BaseRepository takes a generic type for the database key
	public abstract class BaseRepository<T, Key> where T : class
	{
		protected readonly CompanyStoresContext Context;
		protected readonly DbSet<T> DbSet;

		protected BaseRepository(CompanyStoresContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			Context = context;
			DbSet = Context.Set<T>();
		}

		public void Add(T entity)
		{
			DbSet.Add(entity);
		}

		public void Update(T entity)
		{
			DbSet.Attach(entity);
			Context.Entry(entity).State = EntityState.Modified;
		}

		public void Remove(T entity)
		{
			DbSet.Remove(entity);
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await DbSet.ToListAsync();
		}

		public async Task<T> GetByIdAsync(Key id)
		{
			return await DbSet.FindAsync(id);
		}
	}
}
