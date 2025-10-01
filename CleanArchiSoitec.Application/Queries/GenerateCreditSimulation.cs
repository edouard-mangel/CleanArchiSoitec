using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiSoitec.Application.Queries
{
    public class GenerateCreditSimulation : IGenerateCreditSimulation
    {

        public Schedule Execute(CreditSimulationParameters @params)
        {
            return new Schedule(@params.Principal, @params.AnnualRate, @params.DurationInMonths, @params.UnlockDate);
        }
    }
}
