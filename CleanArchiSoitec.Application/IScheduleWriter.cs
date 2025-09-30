using Domain;

namespace CleanArchiSoitec.Application
{
    public interface IScheduleWriter
    {
        void ExportSchedule(Schedule schedule);
    }
}