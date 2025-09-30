using Domain;

namespace UnitTests
{
    internal class ScheduleWriterFake : IScheduleWriter
    {
        public List<Schedule> Schedules { get; internal set; } = new();

        public void ExportSchedule(Schedule schedule)
        {
            Schedules.Add(schedule);
        }
    }
}