using CleanArchiSoitec.Application;
using Domain;

namespace AcceptanceTests
{
    public class ScheduleRepositoryInMemory : IScheduleRepository
    {
        public List<Schedule> Schedules { get; internal set; } = new();
        public ScheduleRepositoryInMemory() 
        {
        }

        public IEnumerable<Schedule> GetAll()
        {
            return Schedules;
        }

        public void Save(Schedule schedule)
        {
            var sc = this.Get(schedule.Id.Value);
            this.Schedules.Remove(sc);
            this.Schedules.Add(new Schedule(schedule.GetSnapShot()));
        }

        public Schedule Get(int id)
        {
            return this.Schedules.First(p => p.Id == id);
        }

        internal void Feed(Schedule schedule)
        {
            this.Schedules.Add(new Schedule(schedule.GetSnapShot()));
        }
    }
}