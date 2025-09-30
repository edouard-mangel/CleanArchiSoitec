namespace Domain
{
    public class RegisterCreditSimulationParameters
    {
        public RegisterCreditSimulationParameters(decimal principal, decimal annualRate, int durationInMonths, DateTime unlockDate)
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