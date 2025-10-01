using Domain;

namespace CleanArchiSoitec.Application.Commands
{
    public interface IRegisterCreditSimulation
    {
        Task<Schedule> Execute(CreditSimulationParameters registerCreditSimulationParameters);
    }
}