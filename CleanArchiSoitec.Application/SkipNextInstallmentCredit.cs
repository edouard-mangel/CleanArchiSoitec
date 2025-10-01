
using Application;
using CleanArchiSoitec.Application;

namespace AcceptanceTests
{
    public class SkipNextInstallmentCredit
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IDateTimeProvider dateTimeProvider;

        public SkipNextInstallmentCredit(IScheduleRepository scheduleRepository, IDateTimeProvider dateTimeProvider)
        {
            this.scheduleRepository = scheduleRepository;
            this.dateTimeProvider = dateTimeProvider;
        }

        public void Execute(int IdToSkip)
        {
            var schedule = scheduleRepository.Get(IdToSkip);
            schedule.SkipInstallmentFromDate(dateTimeProvider.Now());
            this.scheduleRepository.Save(schedule);
        }
    }
}