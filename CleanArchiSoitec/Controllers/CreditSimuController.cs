using CleanArchiSoitec.Infrastructure;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchiSoitec.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditSimuController : ControllerBase
    {
        private readonly IRegisterCreditSimulation register;
        private readonly ILogger<CreditSimuController> _logger;

        public CreditSimuController(IRegisterCreditSimulation register, ILogger<CreditSimuController> logger)
        {
            this.register = register;
            _logger = logger;
        }

        [HttpGet(Name = "Schedule")]
        public CreditSimuResponse Get([FromQuery] CreditSimuRequest request)
        {
            var schedule = register.Execute(
                new RegisterCreditSimulationParameters(request.Principal, request.AnnualRate, request.DurationInMonths, DateTime.Parse(request.UnlockDate))
            );

            var response = new CreditSimuResponse(schedule);
            return response;
        }
    }
}
