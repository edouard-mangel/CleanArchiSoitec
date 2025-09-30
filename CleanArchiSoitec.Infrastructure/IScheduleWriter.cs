using Domain;

namespace CleanArchiSoitec.Infrastructure
{
    public interface IScheduleWriter
    {
        void ExportSchedule(Schedule schedule);
    }
}