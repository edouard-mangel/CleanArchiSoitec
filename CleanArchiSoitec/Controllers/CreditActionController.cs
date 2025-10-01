using CleanArchiSoitec.Application;
using CleanArchiSoitec.Application.Commands;
using CleanArchiSoitec.Application.Queries;
using CleanArchiSoitec.Infrastructure;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchiSoitec.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditActionController : ControllerBase
    {
        private readonly IRegisterCreditSimulation register;
        private readonly IGenerateCreditSimulation generate;
        private readonly ISkipNextInstallmentCredit skipNextInstallment;
        private readonly ILogger<CreditActionController> _logger;

        public CreditActionController(IRegisterCreditSimulation register, ISkipNextInstallmentCredit skipNextInstallment, ILogger<CreditActionController> logger)
        {
            this.register = register;
            this.skipNextInstallment = skipNextInstallment;
            _logger = logger;
        }


        [HttpPost("Register")]
        public IActionResult Post([FromBody] CreditSimuRequest request)
        {
            var schedule = register.Execute(
                new CreditSimulationParameters(request.Principal, request.AnnualRate, request.DurationInMonths, DateTime.Parse(request.UnlockDate))
            );

            return Ok(schedule.Id);
        }

        [HttpPost("SkipNextInvestment")]
        public IActionResult SkipNextInvestment([FromBody] int id)
        {
            skipNextInstallment.Execute(id);
            return Ok();
        }
    }
}
