using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.EMI
{
    public class ScheduleTrial
    {
        public int Day { get; set; }
        public DateTime EnglishDate { get; set; }
        public decimal PrincipalInstallment { get; set; }
        public decimal InterestInstallment { get; set; }
        public decimal Balance { get; set; }
        public decimal TotalInstallment { get; set; }
        public bool HasInterest { get; set; }
        public bool HasPrincipal { get; set; }

        public string Principal { get; set; }
        public string Interest { get; set; }
        public string Total { get; set; }
    }
}
