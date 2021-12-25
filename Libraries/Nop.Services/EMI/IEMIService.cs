using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.EMI
{
    public interface IEMIService
    {
        ScheduleTrial GetScheduleDetails(decimal totalBalance, decimal rate, int months);
    }
}
