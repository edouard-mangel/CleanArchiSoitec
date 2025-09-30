using Domain;

namespace CleanArchiSoitec.Application
{
    public interface IGenerateCreditSimulation
    {
        Schedule Execute(CreditSimulationParameters @params);
    }
}