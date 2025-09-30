using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IScheduleRepository
    {
        public void Save(Schedule schedule);


        public IEnumerable<Schedule> GetAll();


    }
}
