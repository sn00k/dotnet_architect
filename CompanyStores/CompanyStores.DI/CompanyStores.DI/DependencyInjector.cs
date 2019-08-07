using CompanyStores.BLL.Interfaces;
using CompanyStores.BLL.Services;
using CompanyStores.DAL.Interfaces.Infrastructure;
using CompanyStores.DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStores.DI
{
    public class DependencyInjector
    {
        public static void RegisterBLL(SimpleInjector.Container container)
		{
			// register services
			container.Register(typeof(ICompanyService), typeof(CompanyService), SimpleInjector.Lifestyle.Scoped);
			container.Register(typeof(IStoreService), typeof(StoreService), SimpleInjector.Lifestyle.Scoped);
		}

		public static void RegisterDALEntityFramework6(SimpleInjector.Container container, string connectionString)
		{
			// register repositories
			container.Register(typeof(ICompanyRepository), typeof(DAL.EntityFramework6.Repositories.CompanyRepository), SimpleInjector.Lifestyle.Scoped);
			container.Register(typeof(IStoreRepository), typeof(DAL.EntityFramework6.Repositories.StoreRepository), SimpleInjector.Lifestyle.Scoped);

			// register infrastructure
			container.Register(typeof(DAL.EntityFramework6.Infrastructure.CompanyStoresContext), () => new DAL.EntityFramework6.Infrastructure.CompanyStoresContext(connectionString), SimpleInjector.Lifestyle.Scoped);
			container.Register(typeof(IUnitOfWork), typeof(DAL.EntityFramework6.Infrastructure.UnitOfWork), SimpleInjector.Lifestyle.Scoped);
		}
	}
}
