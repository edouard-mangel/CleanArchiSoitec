namespace CleanArchiSoitec.Controllers
{
    public class CreditSimuRequest
    {
        public decimal Principal { get; }
        public decimal AnnualRate { get; }
        public decimal MonthlyAmount { get; }
        public int DurationInMonths { get; }
        public DateTime UnlockDate { get; }
    }
}