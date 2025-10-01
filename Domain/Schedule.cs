namespace Domain;

public class Schedule
{
    public int? Id { get; set; }
    public decimal Principal { get; }
    public decimal AnnualRate { get; }
    public decimal MonthlyAmount { get; }
    public IList<Installment> Installments { get; }
    public int DurationInMonths => Installments.Count();
    private readonly int _InitialDuration;
    public DateTime UnlockDate { get; }

    public Schedule(decimal principal, decimal annualRate, int months, DateTime fromDate)
    {
        Principal = principal;
        AnnualRate = annualRate;
        _InitialDuration = months;
        UnlockDate = fromDate;
        MonthlyAmount = ComputeMonthlyAmount();
        Installments = ComputeSchedule();
    }

    public Schedule(ScheduleSnapshot snapshot)
    {
        this.Id = snapshot.Id;
        this.Principal = snapshot.Principal;
        this.AnnualRate = snapshot.AnnualRate;
        this.UnlockDate = snapshot.UnlockDate;
        this.MonthlyAmount = snapshot.MonthlyAmount;
        this.Installments = snapshot.Installments.Select(i => new Installment(i)).ToList();
    }

    private static decimal ComputePeriodicRate(decimal rate)
    {
        return (decimal)Math.Round(rate /12, 6)/100;
    }

    private decimal ComputeMonthlyAmount()
    {
        decimal numerator = Principal * ComputePeriodicRate(AnnualRate) * (decimal)Math.Pow((double)(1 + ComputePeriodicRate(AnnualRate)), _InitialDuration);
        
        decimal denominator = (decimal)(Math.Pow((double)(1 + ComputePeriodicRate(AnnualRate)), _InitialDuration) - 1);
        
        return Math.Round(numerator/denominator,2);
    }

    private Installment ComputeInstallment(int number, decimal remainingAmount)
    {
        decimal total = MonthlyAmount;
        decimal interest = (decimal)Math.Round( Schedule.ComputePeriodicRate(AnnualRate) * remainingAmount, 2);
        decimal principal = total - interest;

        return new Installment()
        {
            Number = number, 
            Total = total,
            Interest= interest, 
            Principal= principal,
            DateInvest = UnlockDate.AddMonths(number-1)
        };
    }

    public IList<Installment> ComputeSchedule()
    {
        var installments = new List<Installment>();
        decimal remainingAmount = Principal;
        for (int i = 1; i < _InitialDuration; i++)
        {
            Installment currentInstallment = ComputeInstallment(i, remainingAmount);
            remainingAmount -= currentInstallment.Principal;
            installments.Add(currentInstallment);
        }
        remainingAmount = Principal - installments.Sum(i => i.Principal);
        // Last installment adjustment to avoid rounding issues
        installments.Add(ComputeLastInstallment(remainingAmount));

        return installments;
    }

    private Installment ComputeLastInstallment(decimal remainingAmount)
    {
        return new Installment()
        {
            Number = _InitialDuration,
            Principal = remainingAmount,
            Interest = (decimal)Math.Round(Schedule.ComputePeriodicRate(AnnualRate) * remainingAmount, 2),
            Total = remainingAmount + (decimal)Math.Round(Schedule.ComputePeriodicRate(AnnualRate) * remainingAmount, 2),
            DateInvest = UnlockDate.AddMonths(_InitialDuration - 1)
        };
    }

    private Installment GetNextInstallmentFromDate(DateTime dateTime)
    {
        return this.Installments.OrderBy(p => p.Number).First(p => p.DateInvest >= dateTime);
    }

    public void SkipInstallmentFromDate(DateTime dateTime)
    {
        var installmentToSkip = this.GetNextInstallmentFromDate(dateTime);

        
        var installmentSkipped =  new Installment()
        {
            Number = installmentToSkip.Number,
            Principal = 0,
            DateInvest = installmentToSkip.DateInvest,
            Interest = installmentToSkip.Interest,
            Total = installmentToSkip.Interest
        };
        /*
        var installmentLast = Installments.Last();

        var installmentToAdd = new Installment()
        {
            Number = installmentToSkip.Number,
            Principal = 0,
            DateInvest = installmentToSkip.DateInvest,
            Interest = installmentToSkip.Interest,
            Total = installmentToSkip.Interest
        };

        Installments.Remove(installmentToSkip);*/
        Installments.Add(installmentSkipped);
    }

    public ScheduleSnapshot GetSnapShot()
    {
        return new ScheduleSnapshot(Principal, AnnualRate, UnlockDate, MonthlyAmount, Installments.Select(p => new InstallmentSnapshot(p, Id)).ToList(), Id);
    }
}