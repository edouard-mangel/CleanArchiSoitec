namespace UnitTests;

public class CreditSimulatorTest
{
    private CreditSimulator CreditSimulator { get; set; }
    public CreditSimulatorTest()
    {
        CreditSimulator = new(10000, 5, 12);
    }

    [Theory]
    [InlineData(10000, 5, 12, 856.07)]
    [InlineData(20000, 2, 24, 850.81)]
    [InlineData(30000, 3, 120, 289.68)]
    public void MustComputeMonthlyAmount(decimal principal, decimal annualRate, int durationInMonths, decimal expectedMonthlyAmount)
    {
        // Arrange 
        CreditSimulator = new CreditSimulator(principal, annualRate, durationInMonths);

        // Act
        var monthlyAmount = CreditSimulator.ComputeMonthlyAmount();
        
        // Assert
        Assert.Equal(expectedMonthlyAmount, monthlyAmount);
    }

    [Theory]
    [InlineData(10000, 5, 12, 856.07, 41.67, 814.40)]
    [InlineData(30000, 3, 120, 289.68, 75, 214.68)]
    public void MustComputeFirstInstallment(decimal principal, decimal annualRate, int durationInMonths, decimal expectedMonthlyAmount, decimal expectedInterest, decimal expectedPrincipal)
    {
        // Arrange 
        CreditSimulator = new CreditSimulator(principal, annualRate, durationInMonths);

        // Act
        var firstInstallment = CreditSimulator.ComputeInstallment(1, principal);

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
        CreditSimulator = new CreditSimulator(principal, annualRate, durationInMonths);
        
        // Act
        var installments = CreditSimulator.ComputeSchedule();

        // Assert
        Assert.Equal(durationInMonths, installments.Count);
    }

    [Theory]
    [InlineData(10000, 5, 12)]
    [InlineData(20000, 3.5, 24)]
    public void AllIsRefunded(decimal principal, decimal annualRate, int durationInMonths)
    {
        // Arrange 
        CreditSimulator = new CreditSimulator(principal, annualRate, durationInMonths);

        // Act
        var installments = CreditSimulator.ComputeSchedule();

        // Assert 
        decimal TotalRefunded = installments.Sum(i => i.Principal);
        Assert.Equal(principal, TotalRefunded);
    }
}
