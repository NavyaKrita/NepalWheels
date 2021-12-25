using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Catalog
{
    public partial record EMIModel : BaseNopModel
    {
        public EMIModel()
        {
              
        }
        [NopResourceDisplayName("Plugin.Calculator.EMI.Fields.LoanLength")]
        public int Month { get; set; }
        [NopResourceDisplayName("Plugin.Calculator.EMI.Fields.Rate")]
        public decimal Rate { get; set; }
        [NopResourceDisplayName("Plugin.Calculator.EMI.Fields.InitialDownpaymentValue")]
        public decimal Amount { get; set; }
        [NopResourceDisplayName("Plugin.Calculator.EMI.Fields.MonthlyRepayment")]
        public string MonthlyRepayment { get; set; }
       
        public IList<SelectListItem> AvailableMonth { get; set; }
    }
}
