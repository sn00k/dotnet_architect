using CompanyStores.DAL.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.DAL.EntityFramework6.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly CompanyStoresContext _context;

		public UnitOfWork(CompanyStoresContext context)
		{
			if (context == null) { throw new ArgumentNullException(nameof(context)); }

			_context = context;
		}

		public async Task CommitAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
