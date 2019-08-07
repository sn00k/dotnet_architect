using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.DAL.Interfaces.Repositories
{
    public interface IRepository<T, Key> where T : class
	{
		void Add(T entity);
		void Update(T entity);
		void Remove(T entity);
		Task<T> GetByIdAsync(Key id);
		Task<IEnumerable<T>> GetAllAsync();
	}
}
