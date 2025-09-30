using Domain;

namespace CleanArchiSoitec.Application
{
    public interface IRegisterCreditSimulation
    {
        Schedule Execute(CreditSimulationParameters registerCreditSimulationParameters);
    }
}