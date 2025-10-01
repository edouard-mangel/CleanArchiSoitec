
using CleanArchiSoitec.Application;
using CleanArchiSoitec.Application.Commands;
using Domain;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using TestDataGeneration;

namespace AcceptanceTests
{
    public class SkipInstallmentTest
    {
        private Schedule _defaultSchedule;
        private SkipNextInstallmentCredit sut;
        private ScheduleRepositoryInMemory scheduleRepo;
        private ScheduleFinderStub scheduleFinder;
        private DateTimeProviderFake dateTimeProvider;
        public SkipInstallmentTest()
        {
            _defaultSchedule = GenerateDefaultSchedule();
            scheduleRepo = new ScheduleRepositoryInMemory();
            scheduleFinder = new ScheduleFinderStub();
            dateTimeProvider = new DateTimeProviderFake();

            sut = new SkipNextInstallmentCredit(scheduleRepo, scheduleFinder, dateTimeProvider);
        }

        private Schedule GenerateDefaultSchedule()
        {
            return new ScheduleBuilder().Build();
        }

        [Fact]
        public void AddOneInstallment()
        {
            // Arrange 
            
            scheduleFinder.Feed(_defaultSchedule);
            int idToSkip = _defaultSchedule.Id.Value;

            dateTimeProvider.FeedDateTime(_defaultSchedule.UnlockDate.AddMonths(1).AddDays(15));
            // Act
            // Exécuter le UseCase pour ajouter une échéance
            sut.Execute(idToSkip);

            // Assert
            // Récupérer le Schedule dans le repository
            var updatedSchedule = scheduleRepo.Schedules.First(p => p.Id == idToSkip);
            // Vérifier que le Schedule contient bien une échéance de plus
            Assert.Equal(_defaultSchedule.Installments.Count() + 1, updatedSchedule.Installments.Count());
            Assert.Equal(_defaultSchedule.DurationInMonths + 1, updatedSchedule.DurationInMonths);
        }


    }
}
