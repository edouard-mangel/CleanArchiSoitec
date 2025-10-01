using CleanArchiSoitec.Infrastructure.Repositories;
using Domain;

namespace AcceptanceTests
{
    internal class ScheduleFinderStub : IFinder<Schedule>
    {

        public List<Schedule> Schedules { get; internal set; } = new();
        public Schedule Get(int id)
        {
            return Schedules.First(p=> p.Id == id);
        }

        public IReadOnlyCollection<Schedule> GetAll()
        {
            return this.Schedules;
        }

        internal void Feed(Schedule schedule)
        {
            this.Schedules.Add(new Schedule(schedule.GetSnapShot()));
        }
    }
}