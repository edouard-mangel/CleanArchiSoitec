using CleanArchiSoitec.Application;
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
        private readonly IGenerateCreditSimulation generate;
        private readonly ILogger<CreditSimuController> _logger;

        public CreditSimuController(IRegisterCreditSimulation register, IGenerateCreditSimulation generate, ILogger<CreditSimuController> logger)
        {
            this.register = register;
            this.generate = generate;
            _logger = logger;
        }

        [HttpGet(Name = "Schedule")]
        public CreditSimuResponse Get([FromQuery] CreditSimuRequest request)
        {
            var schedule = generate.Execute(
                new CreditSimulationParameters(request.Principal, request.AnnualRate, request.DurationInMonths, DateTime.Parse(request.UnlockDate))
            );

            var response = new CreditSimuResponse(schedule);
            return response;
        }

        [HttpPost(Name = "Schedule")]
        public IActionResult Post([FromBody] CreditSimuRequest request)
        {
            var schedule = register.Execute(
                new CreditSimulationParameters(request.Principal, request.AnnualRate, request.DurationInMonths, DateTime.Parse(request.UnlockDate))
            );

            return Ok();
        }
    }
}
