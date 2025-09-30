using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiSoitec.Application
{
    public class RegisterCreditSimulation : IRegisterCreditSimulation
    {
        private readonly IScheduleWriter _scheduleWriter;

        public RegisterCreditSimulation(IScheduleWriter scheduleWriter)
        {
            _scheduleWriter = scheduleWriter;
        }

        public Schedule Execute(CreditSimulationParameters @params)
        {
            var schedule = new Schedule(@params.Principal, @params.AnnualRate, @params.DurationInMonths, @params.UnlockDate);
            _scheduleWriter.ExportSchedule(schedule);
            return schedule;
        }
    }
}
