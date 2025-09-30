namespace UnitTests;

public class ScheduleTest
{
    private Schedule schedule { get; set; }
    public ScheduleTest()
    {
        schedule = new(10000, 5, 12, new DateTime());
    }

    [Theory]
    [InlineData(10000, 5, 12, 856.07)]
    [InlineData(20000, 2, 24, 850.81)]
    [InlineData(30000, 3, 120, 289.68)]
    public void MustComputeMonthlyAmount(decimal principal, decimal annualRate, int durationInMonths, decimal expectedMonthlyAmount)
    {
        // Arrange 
        schedule = new Schedule(principal, annualRate, durationInMonths, DateTime.Now);

        // Act
        var monthlyAmount = schedule.MonthlyAmount;
        
        // Assert
        Assert.Equal(expectedMonthlyAmount, monthlyAmount);
    }

    [Theory]
    [InlineData(10000, 5, 12, 856.07, 41.67, 814.40)]
    [InlineData(30000, 3, 120, 289.68, 75, 214.68)]
    public void MustComputeFirstInstallment(decimal principal, decimal annualRate, int durationInMonths, decimal expectedMonthlyAmount, decimal expectedInterest, decimal expectedPrincipal)
    {
        // Arrange 
        schedule = new Schedule(principal, annualRate, durationInMonths, DateTime.Now);

        // Act
        var firstInstallment = schedule.Installments.First();

        // Assert
        Assert.Equal(expectedMonthlyAmount, firstInstallment.Total);
        Assert.Equal(expectedInterest, firstInstallment.Interest);
        Assert.Equal(expectedPrincipal, firstInstallment.Principal);
        Assert.Equal(1, firstInstallment.Number);
    }

    [Theory]
    [InlineData(10000, 5, 12)]
    [InlineData(20000, 3.5, 24)]
    public void ComputeAllInstallments(decimal principal, decimal annualRate, int durationInMonths)
    {
        // Arrange 
        schedule = new Schedule(principal, annualRate, durationInMonths, DateTime.Now);

        // Act
        var installments = schedule.Installments;

        // Assert
        Assert.Equal(durationInMonths, installments.Count);
    }

    [Theory]
    [InlineData(10000, 5, 12)]
    [InlineData(20000, 3.5, 24)]
    public void AllIsRefunded(decimal principal, decimal annualRate, int durationInMonths)
    {
        // Arrange 
        schedule = new Schedule(principal, annualRate, durationInMonths, DateTime.Now);

        // Act
        var installments = schedule.Installments;

        // Assert 
        decimal TotalRefunded = installments.Sum(i => i.Principal);
        Assert.Equal(principal, TotalRefunded);
    }
    
    [Theory]
    [InlineData(10000, 5, 12, "2025-12-13")]
    [InlineData(20000, 3.5, 24, "2025-01-14") ]
    public void MustFirstInvestmentDateIsBeginDate(decimal principal, decimal annualRate, int durationInMonths, string dateBegin)
    {
        // Arrange 
        DateTime fromDate = DateTime.Parse(dateBegin);
        schedule = new Schedule(principal, annualRate, durationInMonths, fromDate);

        // Act
        var firstInstallment = schedule.ComputeInstallment(1, principal);

        // Assert
        Assert.Equal(fromDate, firstInstallment.DateInvest);
    }
    [Theory]
    [InlineData(10000, 5, 12, "2025-12-13", "2026-12-13")]
    [InlineData(20000, 3.5, 24, "2025-01-14", "2027-01-14")]
    public void MustEndInvestmentDateAre(decimal principal, decimal annualRate, int durationInMonths, string dateBegin, string expectedValue)
    {
        // Arrange 
        DateTime fromDate = DateTime.Parse(dateBegin);
        schedule = new Schedule(principal, annualRate, durationInMonths, fromDate);

        // Act
        var installments = schedule.Installments;

        // Assert
        DateTime expectedDate = DateTime.Parse(expectedValue);
        Assert.Equal(expectedDate, installments.Last().DateInvest);
    }

    [Theory]
    [InlineData(10000, 5, 12, "2025-12-13")]
    [InlineData(20000, 3.5, 24, "2025-01-31")]
    public void AllInvestsDateAreUnique(decimal principal, decimal annualRate, int durationInMonths, string dateBegin)
    {
        DateTime fromDate = DateTime.Parse(dateBegin);
        // Arrange 
        schedule = new Schedule(principal, annualRate, durationInMonths, fromDate);

        // Act
        var installments = schedule.Installments;

        // Assert 
        Assert.Equal(installments.Count, installments.DistinctBy(p => p.DateInvest.Date).Count());
    }


}
