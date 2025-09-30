using Domain;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchiSoitec.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditSimuController : ControllerBase
    {
        private readonly ILogger<CreditSimuController> _logger;

        public CreditSimuController(ILogger<CreditSimuController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Schedule")]
        public object Get(CreditSimuRequest request)
        {
            Schedule schedule = new Schedule(request.Principal,request.AnnualRate,request.DurationInMonths, request.UnlockDate);
            var response = new CreditSimuResponse(schedule);

            return response;
        }

        
    }
}
