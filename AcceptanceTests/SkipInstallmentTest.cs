using Application;
using CleanArchiSoitec.Application;
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
        private DateTimeProviderFake dateTimeProvider;
        public SkipInstallmentTest()
        {
            _defaultSchedule = GenerateDefaultSchedule();
            scheduleRepo = new ScheduleRepositoryInMemory();
            dateTimeProvider = new DateTimeProviderFake();

            sut = new SkipNextInstallmentCredit(scheduleRepo, dateTimeProvider);


        }

        private Schedule GenerateDefaultSchedule()
        {
            return new ScheduleBuilder().Build();
        }

        [Fact]
        public void AddOneInstallment()
        {
            // Arrange 
            scheduleRepo.Feed(_defaultSchedule);
            int idToSkip = _defaultSchedule.Id.Value;

            dateTimeProvider.FeedDateTime(_defaultSchedule.UnlockDate.AddMonths(1).AddDays(15));
            // Act
            // Exécuter le UseCase pour ajouter une échéance
            sut.Execute(idToSkip);

            // Assert
            // Récupérer le Schedule dans le repository
            var updatedSchedule = scheduleRepo.Get(idToSkip);
            // Vérifier que le Schedule contient bien une échéance de plus
            Assert.Equal(_defaultSchedule.Installments.Count() + 1, updatedSchedule.Installments.Count());
            Assert.Equal(_defaultSchedule.DurationInMonths + 1, updatedSchedule.DurationInMonths);
        }


    }
}
