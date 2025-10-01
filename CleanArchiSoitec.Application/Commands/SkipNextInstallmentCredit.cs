using CleanArchiSoitec.Application;
using CleanArchiSoitec.Infrastructure.Repositories;
using Domain;
using SharedKernel;

namespace CleanArchiSoitec.Application.Commands
{
    public class SkipNextInstallmentCredit : ISkipNextInstallmentCredit
    {
        private readonly IRepository<Schedule> scheduleRepository;
        private readonly IFinder<Schedule> scheduleFinder;
        private readonly IDateTimeProvider dateTimeProvider;

        public SkipNextInstallmentCredit(IRepository<Schedule> scheduleRepository, IFinder<Schedule> scheduleFinder,IDateTimeProvider dateTimeProvider)
        {
            this.scheduleRepository = scheduleRepository;
            this.scheduleFinder = scheduleFinder;
            this.dateTimeProvider = dateTimeProvider;
        }

        public void Execute(int IdToSkip)
        {
            var schedule = scheduleFinder.Get(IdToSkip);
            schedule.SkipInstallmentFromDate(dateTimeProvider.Now());
            scheduleRepository.Save(schedule);
        }
    }
}