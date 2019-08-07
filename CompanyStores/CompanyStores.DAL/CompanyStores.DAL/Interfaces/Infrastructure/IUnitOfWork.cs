using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.DAL.Interfaces.Infrastructure
{
    public interface IUnitOfWork
    {
		Task CommitAsync();
	}
}
