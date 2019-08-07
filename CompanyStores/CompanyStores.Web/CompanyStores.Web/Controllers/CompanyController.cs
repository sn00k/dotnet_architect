using CompanyStores.BLL.Interfaces;
using CompanyStores.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CompanyStores.Web.Controllers
{
	public class CompanyController : BaseController
	{
		private readonly ICompanyService _companyService;
		private readonly IStoreService _storeService;

		public CompanyController(ICompanyService companyService, IStoreService storeService)
		{
			if (companyService == null) throw new ArgumentNullException(nameof(companyService));
			if (storeService == null) throw new ArgumentNullException(nameof(storeService));

			_companyService = companyService;
			_storeService = storeService;
		}

		public async Task<ActionResult> List()
		{
			// AutoMapper would be a better option to
			// map from model -> viewmodel and back.
			var companies = (await _companyService.GetAllAsync()).Select(x => new Company
			{
				Id = x.Id,
				Name = x.Name,
				Notes = x.Notes,
				OrganizationNumber = x.OrganizationNumber
			});

			return View(companies);
		}

		public async Task<ActionResult> Details(Guid id)
		{
			var model = await _companyService.GetByIdAsync(id);

			// viewmodel
			var company = new Company
			{
				Id = model.Id,
				Name = model.Name,
				Notes = model.Notes,
				OrganizationNumber = model.OrganizationNumber
			};

			return View(company);
		}

		[HttpGet]
		public async Task<ActionResult> Edit(Guid id)
		{
			var model = await _companyService.GetByIdAsync(id);

			// viewmodel
			var company = new Company
			{
				Id = model.Id,
				Name = model.Name,
				Notes = model.Notes,
				OrganizationNumber = model.OrganizationNumber
			};

			return View(company);
		}

		[HttpPost]
		public async Task<ActionResult> Edit(string companyJson, string storeJson)//Company vm, Store store)
		{
			if (companyJson == null)
			{
				return Json(new
				{
					success = false
				});
			}

			var company = JsonConvert.DeserializeObject<Company>(companyJson);

			var companyModel = await _companyService.GetByIdAsync(company.Id);

			// company mapping
			companyModel.Name = company.Name;
			companyModel.Notes = company.Notes;
			companyModel.OrganizationNumber = company.OrganizationNumber;

			await _companyService.SaveEditedCompanyAsync(companyModel);

			// if we also want to create a store
			if (storeJson != null)
			{
				var store = JsonConvert.DeserializeObject<Store>(storeJson);

				store.CompanyId = company.Id;

				var storeModel = new BLL.Models.Store
				{
					CompanyId = store.CompanyId,
					Name = store.Name,
					Address = store.Address,
					City = store.City,
					Zip = store.Zip,
					Country = store.Country,
					Longitude = store.Longitude,
					Latitude = store.Latitude
				};

				await _storeService.CreateStoreAsync(storeModel);
			}

			return Json(new { success = true });
		}

		[HttpGet]
		public ActionResult Create()
		{
			var vm = new Company();

			return View(vm);
		}

		[HttpPost]
		public async Task<ActionResult> Create(Company vm)
		{
			if (vm == null || !ModelState.IsValid)
			{
				return Json(new
				{
					success = false,
					responseText = "vm is null or modelstate is invalid."
				});
			}

			var newCompany = new BLL.Models.Company
			{
				Name = vm.Name,
				Notes = vm.Notes,
				OrganizationNumber = vm.OrganizationNumber
			};

			await _companyService.CreateCompanyAsync(newCompany);

			return Json(new
			{
				success = true,
			});
		}

		[HttpGet]
		public async Task<ActionResult> Delete(Guid id)
		{
			if (id == null)
			{
				return Json(new
				{
					success = false,
					responseText = "id is null."
				});
			}

			var model = await _companyService.GetByIdAsync(id);

			// viewmodel
			var company = new Company
			{
				Id = model.Id,
				Name = model.Name,
				Notes = model.Notes,
				OrganizationNumber = model.OrganizationNumber
			};

			return View(company);
		}

		[HttpPost, ActionName("delete")]
		public async Task<ActionResult> DeleteConfirmed(Company vm)
		{
			if (vm == null || vm.Id == null || !ModelState.IsValid)
			{
				return Json(new
				{
					success = false,
					responseText = "vm/id is null or modelstate is invalid."
				});
			}

			var model = await _companyService.GetByIdAsync(vm.Id);

			var stores = await _storeService.GetByCompanyIdAsync(vm.Id);

			foreach (var store in stores)
			{
				await _storeService.DeleteStoreAsync(store);
			}

			await _companyService.DeleteCompanyAsync(model);

			return Json(new
			{
				success = true,
			});
		}
	}
}
