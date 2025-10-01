using CleanArchiSoitec.Application;
using CleanArchiSoitec.Infrastructure.Entities;

using Domain;

using Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiSoitec.Infrastructure.Repositories
{
    public class ScheduleDbRepository : IRepository<Schedule>
    {
        private readonly CreditDbContext dbContext;

        public ScheduleDbRepository(CreditDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task Save(Schedule schedule)
        {
            // Serialization from snapshot
            var snapshot = schedule.GetSnapShot();
            await dbContext.Schedules.AddAsync(new ScheduleEntity()
            {
                Id = snapshot.Id.Value,
                Principal = snapshot.Principal,
                AnnualRate = snapshot.AnnualRate,
                MonthlyAmount = snapshot.MonthlyAmount,
                UnlockDate = snapshot.UnlockDate
            });
            foreach (var installment in schedule.Installments)
            {
                await dbContext.Installments.AddAsync(new InstallmentEntity()
                {
                    Date = installment.DateInvest,
                    Interest = installment.Interest,
                    Principal = installment.Principal,
                    Number = installment.Number,
                    ScheduleId = snapshot.Id.Value,
                    Total = installment.Total
                });
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
