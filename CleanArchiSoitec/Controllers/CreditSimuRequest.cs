namespace CleanArchiSoitec.Controllers
{
    public class CreditSimuRequest
    {
        public decimal Principal { get; set; }
        public decimal AnnualRate { get; set; }
        public int DurationInMonths { get; set; }
        public string UnlockDate { get; set; } = "";
    }
}