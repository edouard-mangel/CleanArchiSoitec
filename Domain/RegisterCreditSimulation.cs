using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RegisterCreditSimulation : IRegisterCreditSimulation
    {
        private readonly IScheduleWriter scheduleWriter;

        public RegisterCreditSimulation(IScheduleWriter scheduleWriter)
        {
            this.scheduleWriter = scheduleWriter;
        }

        public Schedule Execute(RegisterCreditSimulationParameters @params)
        {
            var schedule = new Schedule(@params.Principal, @params.AnnualRate, @params.DurationInMonths, @params.UnlockDate);
            scheduleWriter.ExportSchedule(schedule);
            return schedule;
        }
    }
}
