using Domain;

using TestDataGeneration;

namespace AcceptanceTests
{
    public class SkipInstallmentTest
    {
        private Schedule _defaultSchedule;
        
        public SkipInstallmentTest()
        {
            _defaultSchedule = GenerateDefaultSchedule();
        }

        private Schedule GenerateDefaultSchedule()
        {
            return new ScheduleBuilder().Build();
        }

        [Fact]
        public void AddOneInstallment()
        {
            // Arrange 
            // Instancier le UseCase de la couche Application 
            // Fournir une dépendance qui est capable de simuler la récupération et la sauvegarde d'un Schedule (ex: un repository InMemory)

            // Act
            // Exécuter le UseCase pour ajouter une échéance

            // Assert
            // Récupérer le Schedule dans le repository
            // Vérifier que le Schedule contient bien une échéance de plus
        }


    }
}
