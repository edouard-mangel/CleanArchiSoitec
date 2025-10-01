using Domain;

namespace CleanArchiSoitec.Application.Commands
{
    public interface IRegisterCreditSimulation
    {
        Schedule Execute(CreditSimulationParameters registerCreditSimulationParameters);
    }
}