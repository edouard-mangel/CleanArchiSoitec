using CleanArchiSoitec.Application;
using CleanArchiSoitec.Application.Commands;
using CleanArchiSoitec.Application.Queries;
using CleanArchiSoitec.Infrastructure;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchiSoitec.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditSimuController : ControllerBase
    {
 
        private readonly IGenerateCreditSimulation generate;

        private readonly ILogger<CreditSimuController> _logger;

        public CreditSimuController( IGenerateCreditSimulation generate, ILogger<CreditSimuController> logger)
        {
            this.generate = generate;
            _logger = logger;
        }

        [HttpGet("Schedule")]
        public CreditSimuResponse Get([FromQuery] CreditSimuRequest request)
        {
            var schedule = generate.Execute(
                new CreditSimulationParameters(request.Principal, request.AnnualRate, request.DurationInMonths, DateTime.Parse(request.UnlockDate))
            );

            var response = new CreditSimuResponse(schedule);
            return response;
        }
    }
}
