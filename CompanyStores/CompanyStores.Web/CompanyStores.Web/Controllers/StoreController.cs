using CompanyStores.BLL.Interfaces;
using CompanyStores.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CompanyStores.Web.Controllers
{
    public class StoreController : BaseController
    {
		private readonly IStoreService _storeService;

		public StoreController(IStoreService storeService)
		{
			if (storeService == null) throw new ArgumentNullException(nameof(storeService));

			_storeService = storeService;
		}

		public async Task<ActionResult> List()
		{
			var stores = (await _storeService.GetAllAsync()).Select(x => new Store
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
			});

			return View(stores);
		}

		public ActionResult Create(Guid id)
		{
			var vm = new Store();
			vm.CompanyId = id;

			return PartialView("_CreateStore", vm);
		}

		[HttpGet]
		public async Task<ActionResult> Edit(Guid id)
		{
			var model = await _storeService.GetByIdAsync(id);

			// viewmodel
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

			return View(store);
		}

		[HttpPost]
		public async Task<ActionResult> Edit(Store vm)
		{
			if (vm == null || !ModelState.IsValid)
			{
				return Json(new { success = false });
			}

			var model = await _storeService.GetByIdAsync(vm.Id);

			model.Id = vm.Id;
			model.CompanyId = vm.CompanyId;
			model.Name = vm.Name;
			model.Address = vm.Address;
			model.City = vm.City;
			model.Zip = vm.Zip;
			model.Country = vm.Country;
			model.Longitude = vm.Longitude;
			model.Latitude = vm.Latitude;

			await _storeService.SaveEditedStoreAsync(model);

			return Json(new { success = true });
		}

		public async Task<ActionResult> Details(Guid id)
		{
			var model = await _storeService.GetByIdAsync(id);

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

			return View(store);
		}

		public async Task<ActionResult> Delete(Guid id)
		{
			if (id == null)
			{
				return Json(new { success = false, responseText = "id is null." });
			}

			var model = await _storeService.GetByIdAsync(id);

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

			return View(store);
		}

		[HttpPost, ActionName("delete")]
		public async Task<ActionResult> DeleteConfirmed(Store vm)
		{
			if (vm == null || vm.Id == null || !ModelState.IsValid)
			{
				return Json(new
				{
					success = false,
					responseText = "vm/id is null or modelstate is invalid."
				});
			}

			var model = await _storeService.GetByIdAsync(vm.Id);

			await _storeService.DeleteStoreAsync(model);

			return Json(new { success = true });
		}
    }
}