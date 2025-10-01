using CleanArchiSoitec.Application;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiSoitec.Infrastructure.Repositories
{
    public class ScheduleDbRepository : IRepository<Schedule>
    {
        public ScheduleDbRepository()
        {

        }


        public void Save(Schedule schedule)
        {
            // Serialization from snapshot
            var snapshot = schedule.GetSnapShot();
        }
    }
}
