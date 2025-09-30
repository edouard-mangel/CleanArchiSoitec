namespace CleanArchiSoitec.Application
{
    public class CreditSimulationParameters
    {
        public CreditSimulationParameters(decimal principal, decimal annualRate, int durationInMonths, DateTime unlockDate)
        {
            Principal = principal;
            AnnualRate = annualRate;
            DurationInMonths = durationInMonths;
            UnlockDate = unlockDate;
        }

        public decimal Principal { get; }
        public decimal AnnualRate { get;}
        public int DurationInMonths { get; }
        public DateTime UnlockDate { get; }
    }
}