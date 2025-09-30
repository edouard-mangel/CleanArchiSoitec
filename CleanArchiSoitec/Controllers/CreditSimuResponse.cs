

using Domain;

namespace CleanArchiSoitec.Controllers
{
    internal class CreditSimuResponse
    {
        public Schedule schedule {  get; set; }
        public CreditSimuResponse(Schedule schedule)
        {
            this.schedule = schedule;
        }
    }
}