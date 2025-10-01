using CleanArchiSoitec.Application;
using Domain;

namespace AcceptanceTests
{
    public class ScheduleRepositoryInMemory : IRepository<Schedule>
    {
        public List<Schedule> Schedules { get; private set; } = new();
        public ScheduleRepositoryInMemory() 
        {
        }

        public Task<int> Save(Schedule schedule)
        {
            this.Schedules.Add(new Schedule(schedule.GetSnapShot()));
            return Task.FromResult(1);
        }
    }
}