using CompanyStores.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyStores.DAL.Interfaces.Repositories;
using CompanyStores.DAL.Interfaces.Infrastructure;
using CompanyStores.BLL.Models;
using CompanyStores.BLL.Services.Exceptions;

namespace CompanyStores.BLL.Services
{
	public class StoreService : IStoreService
	{
		private readonly IStoreRepository _storeRepository;
		private readonly IUnitOfWork _unitOfWork;

		public StoreService(IStoreRepository storeRepository, IUnitOfWork unitOfWork)
		{
			if (unitOfWork == null) { throw new ArgumentNullException(nameof(unitOfWork)); }
			if (storeRepository == null) { throw new ArgumentNullException(nameof(storeRepository)); }

			_storeRepository = storeRepository;

			_unitOfWork = unitOfWork;
		}

		public async Task CreateStoreAsync(Store store)
		{
			if (store == null)
			{
				throw new ArgumentNullException(nameof(store) + " is null");
			}

			if (store.CompanyId == Guid.Empty)
			{
				throw new MissingCompanyIdException(nameof(store) + " is missing a company id!");
			}

			var newStore = new DAL.Models.Store
			{
				CompanyId = store.CompanyId,
				Name = store.Name,
				Address = store.Address,
				City = store.City,
				Country = store.Country,
				Zip = store.Zip,
				Longitude = store.Longitude,
				Latitude = store.Latitude
			};

			_storeRepository.Add(newStore);

			await _unitOfWork.CommitAsync();
		}

		public async Task DeleteStoreAsync(Store store)
		{
			var model = await _storeRepository.GetByIdAsync(store.Id);

			_storeRepository.Remove(model);

			await _unitOfWork.CommitAsync();
		}

		public async Task<IList<Store>> GetAllAsync()
		{
			return (await _storeRepository.GetAllAsync()).Select(x => new Store
			{
				Id = x.Id,
				CompanyId = x.CompanyId,
				Name = x.Name,
				Address = x.Address,
				City = x.City,
				Country = x.Country,
				Zip = x.Zip,
				Longitude = x.Longitude,
				Latitude = x.Latitude
			}).ToList();
		}

		public async Task<Store> GetByIdAsync(Guid id)
		{
			var model = await _storeRepository.GetByIdAsync(id);

			var store = new Store
			{
				Id = model.Id,
				CompanyId = model.CompanyId,
				Name = model.Name,
				Address = model.Address,
				City = model.City,
				Zip = model.Zip,
				Country = model.Country,
				Longitude = model.Longitude,
				Latitude = model.Latitude
			};

			return store;
		}

		public async Task<IList<Store>> GetByNameAsync(string name)
		{
			return (await _storeRepository.GetByNameAsync(name)).Select(x => new Store
			{
				Id = x.Id,
				CompanyId = x.CompanyId,
				Name = x.Name,
				Address = x.Address,
				City = x.City,
				Zip = x.Zip,
				Country = x.Country,
				Longitude = x.Longitude,
				Latitude = x.Latitude
			}).ToList();
		}

		public async Task SaveEditedStoreAsync(Store store)
		{
			if (store == null)
			{
				throw new ArgumentNullException(nameof(store) + " is null");
			}

			var model = await _storeRepository.GetByIdAsync(store.Id);

			model.Id = store.Id;
			model.CompanyId = store.CompanyId;
			model.Name = store.Name;
			model.Address = store.Address;
			model.City = store.City;
			model.Zip = store.Zip;
			model.Country = store.Country;
			model.Longitude = store.Longitude;
			model.Latitude = store.Latitude;

			_storeRepository.Update(model);

			await _unitOfWork.CommitAsync();
		}

		public async Task<IEnumerable<Store>> GetByCompanyIdAsync(Guid id)
		{
			if (id == null || id == Guid.Empty)
			{
				throw new MissingCompanyIdException(nameof(id) + " is null or empty");
			}

			var stores = (await _storeRepository.GetByCompanyIdAsync(id)).Select(x => new Store
			{
				Id = x.Id,
				CompanyId = x.CompanyId,
				Name = x.Name,
				Address = x.Address,
				City = x.City,
				Zip = x.Zip,
				Country = x.Country,
				Longitude = x.Longitude,
				Latitude = x.Latitude
			});

			return stores;
		}
	}
}
