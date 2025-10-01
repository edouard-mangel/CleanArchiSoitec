using Domain;

namespace CleanArchiSoitec.Application.Queries
{
    public interface IGenerateCreditSimulation
    {
        Schedule Execute(CreditSimulationParameters @params);
    }
}