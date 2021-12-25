using Microsoft.AspNetCore.Mvc;
using Nop.Services.Vendors;
using Nop.Web.Models.Vendors;
using Nop.Web.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public partial class VendorLocationController : BasePublicController
    {
        private readonly INkVendorLocationModelFactory _nkVendorLocationModelFactory;
        private readonly INkVendorLocationService _nkVendorLocationService;
        public VendorLocationController(INkVendorLocationModelFactory nkVendorLocationModelFactory,
            INkVendorLocationService nkVendorLocationService)
        {
            _nkVendorLocationModelFactory = nkVendorLocationModelFactory;
            _nkVendorLocationService = nkVendorLocationService;

        }
        public virtual async Task<IActionResult> Index()
        {
            var model = await _nkVendorLocationModelFactory.PrepareVendorLocationSearchModelAsync();

            return View("List", model);
        }

        public virtual async Task<IActionResult> List(NkVendorLocationPagingFilteringModel command)
        {
            //prepare model
            var model = await _nkVendorLocationModelFactory.PrepareNkVendorLocationSearchModelAsync(command);

            return View(model);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> GetDistrictByStateId(string stateId, bool? addSelectStateItem, bool? addAsterisk)
        {
            //permission validation is not required here

            // This action method gets called via an ajax request
            if (string.IsNullOrEmpty(stateId))
                throw new ArgumentNullException(nameof(stateId));

            var district = await _nkVendorLocationService.GetDistrictByStateAsync(Convert.ToInt32(stateId));
            var result = (from s in district
                          select new { id = s.Id, name = s.Name }).ToList();
            
                //asterisk
                result.Insert(0, new { id = 0, name = " * " });
            return Json(result);
        }

    }


}

