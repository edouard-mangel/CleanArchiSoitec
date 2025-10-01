using AcceptanceTests;
using CleanArchiSoitec.Application;
using CleanArchiSoitec.Application.Commands;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class RegisterCreditSimulationTest
    {
        private readonly ScheduleWriterFake writer;
        private readonly ScheduleRepositoryInMemory repositoryInMemory;
        private RegisterCreditSimulation sut;

        public RegisterCreditSimulationTest()
        {
            this.writer = new ScheduleWriterFake();
            this.repositoryInMemory = new ScheduleRepositoryInMemory();
            sut = new RegisterCreditSimulation(writer, repositoryInMemory);
        }

        [Theory]
        [InlineData(10000, 5, 12, "2025-12-13")]
        [InlineData(20000, 3.5, 24, "2025-01-31")]
        public async void AllInvestsDateAreUnique(decimal principal, decimal annualRate, int durationInMonths, string dateBegin)
        {
            // Arrange 
            DateTime fromDate = DateTime.Parse(dateBegin);

            var parameterset = new CreditSimulationParameters(principal, annualRate, durationInMonths, fromDate);

            // Act
            var schedule = await sut.Execute(parameterset);

            // Assert 
            
            var writtenSchedule = this.writer.Schedules.First();
            Assert.Equal(schedule, writtenSchedule);
        }

        [Theory]
        [InlineData(10000, 5, 12, 856.07)]
        [InlineData(20000, 2, 24, 850.81)]
        [InlineData(30000, 3, 120, 289.68)]
        public async void MustComputeMonthlyAmount(decimal principal, decimal annualRate, int durationInMonths, decimal expectedMonthlyAmount)
        {
            // Arrange 
            DateTime fromDate = DateTime.Now;

            var parameterset = new CreditSimulationParameters(principal, annualRate, durationInMonths, fromDate);

            // Act
            var schedule = await sut.Execute(parameterset);
            var monthlyAmount = schedule.MonthlyAmount;

            // Assert
            Assert.Equal(expectedMonthlyAmount, monthlyAmount);
        }

        [Theory]
        [InlineData(10000, 5, 12, 856.07, 41.67, 814.40)]
        [InlineData(30000, 3, 120, 289.68, 75, 214.68)]
        public async void MustComputeFirstInstallment(decimal principal, decimal annualRate, int durationInMonths, decimal expectedMonthlyAmount, decimal expectedInterest, decimal expectedPrincipal)
        {
            // Arrange 
            DateTime fromDate = DateTime.Now;

            var parameterset = new CreditSimulationParameters(principal, annualRate, durationInMonths, fromDate);

            // Act
            var schedule = await sut.Execute(parameterset);
            var firstInstallment = schedule.Installments.First();

            // Assert
            Assert.Equal(expectedMonthlyAmount, firstInstallment.Total);
            Assert.Equal(expectedInterest, firstInstallment.Interest);
            Assert.Equal(expectedPrincipal, firstInstallment.Principal);
            Assert.Equal(1, firstInstallment.Number);
        }

        [Theory]
        [InlineData(10000, 5, 12)]
        [InlineData(20000, 3.5, 24)]
        public async void ComputeAllInstallments(decimal principal, decimal annualRate, int durationInMonths)
        {
            // Arrange 
            DateTime fromDate = DateTime.Now;

            var parameterset = new CreditSimulationParameters(principal, annualRate, durationInMonths, fromDate);

            // Act
            var schedule = await sut.Execute(parameterset);
            var installments = schedule.Installments;

            // Assert
            Assert.Equal(durationInMonths, installments.Count);
        }

        [Theory]
        [InlineData(10000, 5, 12)]
        [InlineData(20000, 3.5, 24)]
        public async void AllIsRefunded(decimal principal, decimal annualRate, int durationInMonths)
        {
            // Arrange 
            DateTime fromDate = DateTime.Now;

            var parameterset = new CreditSimulationParameters(principal, annualRate, durationInMonths, fromDate);

            // Act
            var schedule = await sut.Execute(parameterset);
            var installments = schedule.Installments;

            // Assert 
            decimal TotalRefunded = installments.Sum(i => i.Principal);
            Assert.Equal(principal, TotalRefunded);
        }

        [Theory]
        [InlineData(10000, 5, 12, "2025-12-13")]
        [InlineData(20000, 3.5, 24, "2025-01-14")]
        public async void MustFirstInvestmentDateIsBeginDate(decimal principal, decimal annualRate, int durationInMonths, string dateBegin)
        {
            // Arrange 
            DateTime fromDate = DateTime.Now;

            var parameterset = new CreditSimulationParameters(principal, annualRate, durationInMonths, fromDate);

            // Act
            var schedule = await sut.Execute(parameterset);
            var firstInstallment = schedule.Installments.First();

            // Assert
            Assert.Equal(fromDate, firstInstallment.DateInvest);
        }
        [Theory]
        [InlineData(10000, 5, 12, "2025-12-13", "2026-11-13")]
        [InlineData(20000, 3.5, 24, "2025-01-14", "2026-12-14")]
        public async void MustEndInvestmentDateAre(decimal principal, decimal annualRate, int durationInMonths, string dateBegin, string expectedValue)
        {// Arrange 
            DateTime fromDate = DateTime.Parse(dateBegin);

            var parameterset = new CreditSimulationParameters(principal, annualRate, durationInMonths, fromDate);

            // Act
            var schedule = await sut.Execute(parameterset);
            var installments = schedule.Installments;

            // Assert
            DateTime expectedDate = DateTime.Parse(expectedValue);
            Assert.Equal(expectedDate, installments.Last().DateInvest);
        }
    }
}
