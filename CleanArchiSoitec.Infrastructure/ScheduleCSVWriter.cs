using Domain;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchiSoitec.Infrastructure
{
    public class ScheduleCSVWriter
    {
        private readonly Schedule schedule;

        public ScheduleCSVWriter(Schedule schedule)
        {
            this.schedule = schedule;

        }

        public void ExportSchedule()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"{DateTime.Now.ToString("yyyyMMddhhmmssfff")}.csv");


            var sb = new StringBuilder();

            // En-tête
            sb.AppendLine("Number;DateInvest;Principal;Interest");

            foreach (var installment in schedule.Installments)
            {
                sb.AppendLine($"{installment.Number};{installment.DateInvest};{installment.Principal};{installment.Interest}");
            }

            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        }

    }
}
