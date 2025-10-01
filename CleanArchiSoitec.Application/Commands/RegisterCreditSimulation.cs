using Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiSoitec.Application.Commands
{
    public class RegisterCreditSimulation : IRegisterCreditSimulation
    {
        private readonly IScheduleWriter _scheduleWriter;
        private readonly IRepository<Schedule> repository;

        public RegisterCreditSimulation(IScheduleWriter scheduleWriter, IRepository<Schedule> repository)
        {
            _scheduleWriter = scheduleWriter;
            this.repository = repository;
        }

        public async Task<Schedule> Execute(CreditSimulationParameters @params)
        {
            var schedule = new Schedule(@params.Principal, @params.AnnualRate, @params.DurationInMonths, @params.UnlockDate);
            _scheduleWriter.ExportSchedule(schedule);
            int newId = await repository.Save(schedule);
            schedule.Id = newId;

            return schedule;
        }
    }
}
