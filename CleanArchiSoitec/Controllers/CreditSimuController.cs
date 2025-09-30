using CleanArchiSoitec.Infrastructure;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchiSoitec.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditSimuController : ControllerBase
    {
        private readonly IScheduleWriter scheduleWriter;
        private readonly ILogger<CreditSimuController> _logger;

        public CreditSimuController(IScheduleWriter scheduleWriter, ILogger<CreditSimuController> logger)
        {
            this.scheduleWriter = scheduleWriter;
            _logger = logger;
        }

        [HttpGet(Name = "Schedule")]
        public CreditSimuResponse Get([FromQuery] CreditSimuRequest request)
        {
            Schedule schedule = new Schedule(request.Principal,request.AnnualRate,request.DurationInMonths, DateTime.Parse(request.UnlockDate));
            var response = new CreditSimuResponse(schedule);
            scheduleWriter.ExportSchedule(schedule);
            return response;
        }
    }
}
