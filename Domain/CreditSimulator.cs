
namespace UnitTests;

public class CreditSimulator
{
    public decimal Principal { get; }
    public decimal AnnualRate { get; }
    public decimal MonthlyAmount { get; }
    public int DurationInMonths { get; }

    public CreditSimulator(decimal principal, decimal annualRate, int months)
    {
        Principal = principal;
        AnnualRate = annualRate;
        DurationInMonths = months;
        MonthlyAmount = ComputeMonthlyAmount();
    }

    private static decimal ComputePeriodicRate(decimal rate)
    {
        return (decimal)Math.Round(rate /12, 6)/100;
    }

    public decimal ComputeMonthlyAmount()
    {
        decimal numerator = Principal * ComputePeriodicRate(AnnualRate) * (decimal)Math.Pow((double)(1 + ComputePeriodicRate(AnnualRate)), DurationInMonths);
        
        decimal denominator = (decimal)(Math.Pow((double)(1 + ComputePeriodicRate(AnnualRate)), DurationInMonths) - 1);
        
        return Math.Round(numerator/denominator,2);
    }

    public Installment ComputeInstallment(int number, decimal remainingAmount, DateTime fromDate)
    {
        decimal total = MonthlyAmount;
        decimal interest = (decimal)Math.Round( CreditSimulator.ComputePeriodicRate(AnnualRate) * remainingAmount, 2);
        decimal principal = total - interest;

        return new Installment()
        {
            Number = number, 
            Total = total,
            Interest= interest, 
            Principal= principal,
            DateInvest = fromDate.AddMonths(number-1)
        };
    }

    public IReadOnlyCollection<Installment> ComputeSchedule(DateTime fromDate)
    {
        var installments = new List<Installment>();
        decimal remainingAmount = Principal;
        for (int i = 1; i < DurationInMonths; i++)
        {
            Installment currentInstallment = ComputeInstallment(i, remainingAmount, fromDate);
            remainingAmount -= currentInstallment.Principal;
            installments.Add(currentInstallment);
        }
        remainingAmount = Principal - installments.Sum(i => i.Principal);
        // Last installment adjustment to avoid rounding issues
        installments.Add(ComputeLastInstallment(fromDate, remainingAmount));

        return installments;
    }

    private Installment ComputeLastInstallment(DateTime fromDate, decimal remainingAmount)
    {
        return new Installment()
        {
            Number = DurationInMonths,
            Principal = remainingAmount,
            Interest = (decimal)Math.Round(CreditSimulator.ComputePeriodicRate(AnnualRate) * remainingAmount, 2),
            Total = remainingAmount + (decimal)Math.Round(CreditSimulator.ComputePeriodicRate(AnnualRate) * remainingAmount, 2),
            DateInvest = fromDate.AddMonths(DurationInMonths)
        };
    }
}