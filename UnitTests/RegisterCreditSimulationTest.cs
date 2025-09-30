using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class RegisterCreditSimulationTest
    {
        private readonly ScheduleWriterFake writer;
        private RegisterCreditSimulation sut;

        public RegisterCreditSimulationTest()
        { 
            this.writer = new ScheduleWriterFake();
            sut = new RegisterCreditSimulation(writer);
        }

        [Theory]
        [InlineData(10000, 5, 12, "2025-12-13")]
        [InlineData(20000, 3.5, 24, "2025-01-31")]
        public void AllInvestsDateAreUnique(decimal principal, decimal annualRate, int durationInMonths, string dateBegin)
        {            
            // Arrange 
            DateTime fromDate = DateTime.Parse(dateBegin);

            var parameterset = new RegisterCreditSimulationParameters(principal, annualRate, durationInMonths, fromDate);

            // Act
            var schedule = sut.Execute(parameterset);

            // Assert 

            var writtenSchedule = this.writer.Schedules.First();
            Assert.Equal(schedule,writtenSchedule);
        }
    }
}
