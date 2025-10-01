using CleanArchiSoitec.Application;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiSoitec.Infrastructure
{
    public class ScheduleRepository : IScheduleRepository
    {
        public ScheduleRepository()
        {

        }

        public Schedule Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Schedule> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(Schedule schedule)
        {
            // Serialization from snapshot
            var snapshot = schedule.GetSnapShot();
            
        }
    }
}
