using Nop.Core;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Calculator.EMI
{
    public class EMIProvider : BasePlugin
    {
        #region Fields      
        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        #endregion
        #region Ctor

        public EMIProvider(ILocalizationService localizationService,
            IWebHelper webHelper)
        {
            _localizationService = localizationService;
            _webHelper = webHelper;
        }
        #endregion

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/EMI/Configure";
        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task InstallAsync()
        {
            //locales
            await _localizationService.AddLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugin.Calculator.EMI.TwelveMonths"] = "12  Months",
                ["Plugin.Calculator.EMI.TwentyFourMonths"] = "24 Months",
                ["Plugin.Calculator.EMI.CalculateEMI"] = "Calculate EMI",
                ["Plugin.Calculator.EMI.Fields.SelectModel"] = "Select Model",
                ["Plugin.Calculator.EMI.Fields.LoanLength"] = "Loan Length",
                ["Plugin.Calculator.EMI.Fields.Rate"] = "Interest Rate",
                ["Plugin.Calculator.EMI.Fields.InitialDownpaymentValue"] = "Initial Downpayment Value",
                ["Plugin.Calculator.EMI.Fields.MonthlyRepayment"] = "Monthly Repayment",
                ["Plugin.Calculator.EMI.Fields.Price"] = "Price",
                ["Plugin.Calculator.EMI.Fields.Calculate"] = "Calculate",

            });

            await base.InstallAsync();
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task UninstallAsync()
        {
            //locales
            await _localizationService.DeleteLocaleResourcesAsync("Plugin.Calculator.EMI");

            await base.UninstallAsync();
        }
    }
}
