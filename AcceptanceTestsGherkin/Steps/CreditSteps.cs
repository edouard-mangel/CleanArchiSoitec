using CleanArchiSoitec.Controllers;

using FluentAssertions;
using Reqnroll;

namespace AcceptanceTests.Steps;

[Binding]
public class CreditSteps(ScenarioContext context) : BaseSteps(context)
{
    [When("I simulate a credit with amount {int}, duration {int} months and interest rate {int}%")]
    public async Task WhenISimulateACreditWithAmountDurationMonthsAndInterestRate(int p0, int p1, int p2)
    {
        var request = new CreditSimuRequest
        {
            Principal = p0,
            DurationInMonths = p1,
            AnnualRate = p2,
            UnlockDate = "2025/01/01"
        };

        CreditSimuResponse value = await this.CreditRequests.GetSimulation(request);
        this.Context.Add("simulationResult", value);
    }


    [Then("The monthly payment is (.+?)")]
    public void ThenTheMonthlyPaymentIs(decimal monthlyAmount)
    {
        this.Context.Get<CreditSimuResponse>("simulationResult").schedule.MonthlyAmount.Should().Be(monthlyAmount);
    }



}
