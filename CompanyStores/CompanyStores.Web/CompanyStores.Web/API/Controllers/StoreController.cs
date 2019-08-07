using CompanyStores.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace CompanyStores.Web.API.Controllers
{
	// this should really be in its own .API project
	// but in the interest of time... :)
    public class StoreController : ApiController
    {
		private readonly IStoreService _storeService;

		public StoreController(IStoreService storeService)
		{
			if (storeService == null) throw new ArgumentNullException(nameof(storeService));

			_storeService = storeService;
		}

		public async Task<string> Get(Guid id)
		{
			var store = await _storeService.GetByIdAsync(id);

			var serializer = new JavaScriptSerializer();

			return serializer.Serialize(store);
		}
    }
}
