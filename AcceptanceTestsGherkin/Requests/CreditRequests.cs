using AcceptanceTests.Configuration;

using CleanArchiSoitec.Controllers;


namespace AcceptanceTests.Requests
{
    public class CreditRequests(AcceptanceClient client)
    {
        public Task<Guid> SkipNextInstallment(int creditId)
        {
            return client.Post<Guid>($"api/Credit/{creditId}/skipNext");
        }

        public Task<CreditSimuResponse> GetSimulation(CreditSimuRequest request)
        {
            return client.Get<CreditSimuResponse>($"api/CreditSimulation/Schedule?Principal={request.Principal}&DurationInMonths={request.DurationInMonths}&AnnualRate={request.AnnualRate}");
        }

    }
}
