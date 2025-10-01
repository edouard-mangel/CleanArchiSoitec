using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using CleanArchiSoitec.Infrastructure.Repositories;
using Domain;

namespace ArchUnit;

public class DomainTests
{
    private static readonly Architecture Architecture =
    new ArchLoader().LoadAssemblies(typeof(Schedule).Assembly, typeof(ScheduleCSVWriter).Assembly).Build();
    
    [Fact]
    public void DomainMustNotHaveDependencyToInfrastructure()
    {
        IArchRule rule = ArchRuleDefinition.Classes()
            .That()
            .ResideInNamespace("Domain")
            .Should()
            .NotDependOnAnyTypesThat()
            .ResideInNamespace("CleanArchiSoitec.Infrastructure")
            .AndShould()
            .NotDependOnAnyTypesThat()
            .ResideInNamespace("UnitTests");

        Assert.True(rule.HasNoViolations(Architecture));
    }
}
