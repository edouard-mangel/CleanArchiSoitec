using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArchiSoitec.Infrastructure
{
    public class ScheduleJSONWriter : IScheduleWriter
    {

        public void ExportSchedule(Schedule schedule)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"{DateTime.Now.ToString("yyyyMMddhhmmssfff")}.json");
            string json = JsonSerializer.Serialize(schedule.Installments);

            File.WriteAllText(path, json, Encoding.UTF8);
        }
    }
}
