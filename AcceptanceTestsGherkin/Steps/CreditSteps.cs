using CleanArchiSoitec.Controllers;

using Reqnroll;

namespace AcceptanceTests.Steps;

[Binding]
public class CreditSteps(ScenarioContext context) : BaseSteps(context)
{
    [When("I simulate a credit with amount {int}, duration {int} months and interest rate {float}%")]
    public async Task WhenISimulateACreditWithAmountDurationMonthsAndInterestRate(int amount, int duration, decimal ratePercent)
    {
        var request = new CreditSimuRequest
        {
            Principal = amount,
            DurationInMonths = duration,
            AnnualRate = ratePercent,
            UnlockDate = "2025/01/01"
        };

        CreditSimuResponse value = await this.CreditRequests.GetSimulation(request);
        this.Context.Add("simulationResult", value);
    }


    [Then("the monthly payment should be {float}")]
    public void ThenTheMonthlyPaymentShouldBe(Decimal monthlyAmount)
    {
        Assert.Equal(monthlyAmount, this.Context.Get<CreditSimuResponse>("simulationResult").MonthlyAmount);
    }

    [Then("the number of installments is {int}")]
    public void ThenTheNumberOfInstallmentsIs(int p0)
    {
        Assert.Equal(p0, this.Context.Get<CreditSimuResponse>("simulationResult").DurationInMonths);
    }

}
