using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Calculator.EMI.Model;
using Nop.Services.EMI;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Calculator.EMI.Controllers
{
    [Area(AreaNames.Admin)]
    [AuthorizeAdmin]
    [AutoValidateAntiforgeryToken]
    public class EMIController : BasePluginController
    {
        #region Fields
        private readonly IEMIService _emiService;
        private readonly IPermissionService _permissionService;
        private readonly ILocalizationService _localizationService;
        #endregion

        #region Ctor
        public EMIController(IEMIService emiService,
            IPermissionService permissionService,
            ILocalizationService localizationService)
        {
            _emiService = emiService;
            _permissionService = permissionService;
            _localizationService = localizationService;
        }
        #endregion

        #region Methods

        /// <returns>A task that represents display views</returns>
        public async Task<IActionResult> Configure(decimal? productAmount, decimal? interestRate)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageEMICalculater))
                return AccessDeniedView();
            var availableMonth = new List<SelectListItem>()
            {
                new SelectListItem { Text = await _localizationService.GetResourceAsync("Plugin.Calculator.EMI.TwelveMonths"), Value = "12" },
                new SelectListItem { Text = await _localizationService.GetResourceAsync("Plugin.Calculator.EMI.TwentyFourMonths"), Value = "24" }
            };

            var model = new EMIModel()
            {
                AvailableMonth = availableMonth,
                Rate = interestRate ?? 0,
                Amount = productAmount ?? 0


            };

            //prepare model

            return View("~/Plugins/Calculator.EMI/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task<IActionResult> Calculate(EMIModel emiModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageEMICalculater))
                return await AccessDeniedDataTablesJson();

            //prepare model
            var model = _emiService.GetScheduleDetails(emiModel.Amount, emiModel.Rate, emiModel.Month);

            return Json(model);
        }
        #endregion
    }
}

