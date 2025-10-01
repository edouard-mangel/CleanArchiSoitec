using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiSoitec.Application
{
    public interface IRepository<T>
    {
        public Task Save(T schedule);


    }
}
