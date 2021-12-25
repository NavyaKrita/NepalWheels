using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Calculator.EMI.Model
{
   public class EMIModel
    {
        
        [NopResourceDisplayName("Plugin.Calculator.EMI.Fields.LoanLength")]
        public int Month { get; set; }
        [NopResourceDisplayName("Plugin.Calculator.EMI.Fields.Rate")]
        public decimal Rate { get; set; }
        [NopResourceDisplayName("Plugin.Calculator.EMI.Fields.InitialDownpaymentValue")]
        public decimal Amount { get; set; }
        [NopResourceDisplayName("Plugin.Calculator.EMI.Fields.MonthlyRepayment")]
        public decimal MonthlyRepayment { get; set; }
        public IList<SelectListItem> AvailableMonth { get; set; }
    }
}
