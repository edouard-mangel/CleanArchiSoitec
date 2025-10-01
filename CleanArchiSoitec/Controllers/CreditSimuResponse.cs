

using Domain;

namespace CleanArchiSoitec.Controllers
{
    public class CreditSimuResponse
    {
        public CreditSimuResponse()
        {
                
        }
        public decimal Principal { get; set; }
        public decimal AnnualRate { get; set; }
        public decimal MonthlyAmount { get; set; }
        public int DurationInMonths { get; set; }

        public CreditSimuResponse(Schedule schedule)
        {
            Principal = schedule.Principal;
            AnnualRate = schedule.AnnualRate;
            MonthlyAmount = schedule.MonthlyAmount;
            DurationInMonths = schedule.DurationInMonths;
        }
    }
}