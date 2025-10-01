using CleanArchiSoitec.Application;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiSoitec.Infrastructure.Repositories
{
    public class ScheduleDbFinder :  IFinder<Schedule>
    {
        public ScheduleDbFinder() { }

        public Schedule Get(int id)
        {
            // use db context
            return null;
        }

        public IReadOnlyCollection<Schedule> GetAll()
        {
            // use db context
            return new List<Schedule>();
        }

    }
}
