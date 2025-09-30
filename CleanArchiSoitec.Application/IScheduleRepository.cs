using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiSoitec.Application
{
    public interface IScheduleRepository
    {
        public void Save(Schedule schedule);


        public IEnumerable<Schedule> GetAll();


    }
}
