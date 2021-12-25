
using Nop.Services.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.EMI
{
    public class EMIService : IEMIService
    {
        private readonly IPriceFormatter _priceFormatter;
        public EMIService(IPriceFormatter priceFormatter)
        {
            _priceFormatter = priceFormatter;
        }

        #region Loan schedule calculation
        public ScheduleTrial GetScheduleDetails(decimal totalBalance, decimal rate, int months)
        {
            DateTime matureDate = default(DateTime);
            DateTime todays = DateTime.Today;

            DateTime startDate = new DateTime(todays.Year, todays.Month, 1);
            try
            {
                matureDate = GetMatureDate(startDate, months);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid maturity date.!!");

            }
            List<ScheduleTrial> scheduleTrial = new List<ScheduleTrial>();

            scheduleTrial = this.DistributeDate(startDate, matureDate, 1);
            scheduleTrial = this.InsertDays(startDate, scheduleTrial);
            //Equal Principal
            scheduleTrial = this.GetEqualInstallment(scheduleTrial, totalBalance, rate, false);
            return scheduleTrial.Select(x => new ScheduleTrial
            {

                PrincipalInstallment = Math.Round(x.PrincipalInstallment, 2),
                InterestInstallment = Math.Round(x.InterestInstallment, 2),
                Interest = _priceFormatter.FormatPriceAsync(Math.Round(x.InterestInstallment, 2)).Result,
                Principal = _priceFormatter.FormatPriceAsync(Math.Round(x.PrincipalInstallment, 2)).Result,
                Total = _priceFormatter.FormatPriceAsync(Math.Round(x.TotalInstallment, 2)).Result,
                Day = x.Day,
                TotalInstallment = Math.Round(x.TotalInstallment, 2)
            }).FirstOrDefault();
        }

        #region private method
        private List<ScheduleTrial> InsertDays(DateTime startDate, List<ScheduleTrial> scheduleTrial)
        {
            List<ScheduleTrial> scheduleTrialDays = new List<ScheduleTrial>();
            int i = 0;
            foreach (var item in scheduleTrial)
            {

                ScheduleTrial schedule = new ScheduleTrial();
                schedule = item;
                if (i == 0)
                {
                    schedule.Day = (item.EnglishDate - startDate).Days + 1;
                }
                else
                {
                    schedule.Day = (item.EnglishDate - startDate).Days;
                }
                startDate = item.EnglishDate;
                scheduleTrialDays.Add(schedule);
                i++;
            }
            return scheduleTrialDays;
        }
        private List<ScheduleTrial> DistributeDate(DateTime startDate, DateTime matureDate, int frequency)
        {
            try
            {
                List<ScheduleTrial> ScheduleList = new List<ScheduleTrial>();

                bool hasInterest = true;
                bool hasPrincipal = true;

                DateTime installmentDate = startDate;

                while (installmentDate < matureDate)
                {
                    installmentDate = GetNextInstallmentDate(installmentDate, frequency);
                    if (installmentDate > matureDate)
                    {
                        break;
                    }
                    else
                    {
                        ScheduleTrial scheduleTrial = new ScheduleTrial();
                        scheduleTrial.EnglishDate = installmentDate;
                        scheduleTrial.HasInterest = hasInterest;
                        scheduleTrial.HasPrincipal = hasPrincipal;
                        ScheduleList.Add(scheduleTrial);
                    }
                }

                if (ScheduleList.Select(x => x.EnglishDate).LastOrDefault() < matureDate)
                {
                    var lastScheduleList = new ScheduleTrial();
                    lastScheduleList.EnglishDate = matureDate;
                    lastScheduleList.HasInterest = hasInterest;
                    lastScheduleList.HasPrincipal = hasPrincipal;
                    ScheduleList.Add(lastScheduleList);
                }
                else
                {
                    var removeRow = ScheduleList.LastOrDefault();
                    var lastScheduleList = new ScheduleTrial();
                    lastScheduleList.EnglishDate = matureDate;
                    lastScheduleList.HasInterest = hasInterest;
                    lastScheduleList.HasPrincipal = hasPrincipal;
                    ScheduleList[ScheduleList.FindIndex(x => x.EnglishDate == removeRow.EnglishDate)] = lastScheduleList;
                }
                return ScheduleList;


            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private List<ScheduleTrial> GetEqualInstallment(List<ScheduleTrial> scheduleTrial, decimal principal, decimal rate, bool isFlat)
        {

            try
            {
                decimal totalAmount = principal;
                decimal balanceAmount = 0;

                decimal divideAmount = 0;

                decimal interestInstallment = 0;
                decimal principalInstallment = 0;


                int rowNumber = default(int);
                int counter = default(int);

                int principalCount = 0;
                principalCount = scheduleTrial.Where(x => x.HasPrincipal == true).Count();
                divideAmount = (principal / principalCount);

                rowNumber = scheduleTrial.Count() - 1;

                decimal lastPrincipalBalance = 1;

                int nLoop = 0;

                while (lastPrincipalBalance != 0)
                {
                    var mcalSchedule = scheduleTrial.FirstOrDefault();
                    balanceAmount = totalAmount;

                    principal = totalAmount;

                    interestInstallment = Math.Round(((balanceAmount * rate * mcalSchedule.Day) / 36500), 4);

                    if (mcalSchedule.HasPrincipal == false)
                    {
                        principalInstallment = 0;
                    }
                    else
                    {
                        principalInstallment = divideAmount - interestInstallment;
                    }

                    principal = principal - principalInstallment;
                    scheduleTrial[0].PrincipalInstallment = principalInstallment;
                    scheduleTrial[0].InterestInstallment = interestInstallment;
                    scheduleTrial[0].TotalInstallment = principalInstallment + interestInstallment;
                    scheduleTrial[0].Balance = balanceAmount - principalInstallment;

                    balanceAmount = balanceAmount - principalInstallment;

                    for (counter = 0; counter <= rowNumber - 1; counter++)
                    {
                        if (isFlat == true)
                        {
                            interestInstallment = Math.Round(((totalAmount * rate * scheduleTrial[counter + 1].Day) / 36500), 4);
                            //Other
                        }
                        else
                        {
                            interestInstallment = Math.Round((balanceAmount * rate * scheduleTrial[counter + 1].Day) / 36500, 4);
                        }

                        if (scheduleTrial[counter + 1].HasPrincipal == false)
                        {
                            principalInstallment = 0;
                        }
                        else
                        {
                            principalInstallment = divideAmount - interestInstallment;
                        }

                        principal = principal - principalInstallment;
                        scheduleTrial[counter + 1].PrincipalInstallment = principalInstallment;
                        scheduleTrial[counter + 1].InterestInstallment = interestInstallment;
                        scheduleTrial[counter + 1].TotalInstallment = principalInstallment + interestInstallment;
                        scheduleTrial[counter + 1].Balance = balanceAmount - principalInstallment;
                        balanceAmount = balanceAmount - principalInstallment;

                    }

                    if (lastPrincipalBalance == scheduleTrial.Select(x => x.Balance).LastOrDefault())
                    {
                        lastPrincipalBalance = 0;
                    }
                    else
                    {
                        lastPrincipalBalance = scheduleTrial.Select(x => x.Balance).LastOrDefault();
                    }


                    if (principal > Convert.ToDecimal(0.0000))
                    {
                        if (rowNumber == 1)
                        {
                            divideAmount = divideAmount + (principal / Convert.ToDecimal(1.4));
                        }
                        else
                        {
                            divideAmount = divideAmount + (principal / (rowNumber + 1));
                        }
                    }
                    else
                    {
                        if (rowNumber == 1)
                        {
                            divideAmount = (divideAmount - (Math.Abs(principal) / Convert.ToDecimal(1.4)));
                        }
                        else
                        {
                            divideAmount = (divideAmount - Math.Abs(principal) / (rowNumber + 1));
                        }
                    }

                    nLoop = nLoop + 1;
                    if (nLoop > 100)
                    {
                        break; // TODO: might not be correct. Was : Exit While
                    }
                }

                //Adjusting on last installment
                if (scheduleTrial[counter].Balance != Convert.ToDecimal(0.0000))
                {
                    decimal deviation = 0;
                    deviation = 0 - scheduleTrial[counter].Balance;
                    counter = Convert.ToInt16(scheduleTrial.Count() - 1);
                    scheduleTrial[counter].TotalInstallment = scheduleTrial[counter].TotalInstallment - deviation;
                    scheduleTrial[counter].PrincipalInstallment = scheduleTrial[counter].PrincipalInstallment - deviation;
                    scheduleTrial[counter].Balance = scheduleTrial[counter].Balance;
                }

                return scheduleTrial;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        private DateTime GetMatureDate(DateTime StartDate, int time)
        {
            DateTime dateTime = default(DateTime);
            dateTime = StartDate.AddMonths(time);
            dateTime = dateTime.AddDays(-1);

            return dateTime;
        }
        private DateTime GetNextInstallmentDate(DateTime CurrentDate, int Frequency)
        {
            try
            {

                DateTime dateTime = default(DateTime);
                int daysInMonth = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
                if (daysInMonth != CurrentDate.Day)
                {
                    Frequency = Frequency - 1;
                }
                dateTime = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
                dateTime = dateTime.AddMonths(Frequency);
                dateTime = new DateTime(dateTime.Year, dateTime.Month, System.DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
                return dateTime;
            }
            catch (Exception exc)
            {

                throw;
            }

        }
        #endregion
        #endregion
    }
}
