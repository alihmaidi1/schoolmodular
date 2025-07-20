using FluentAssertions;
using NetArchTest.Rules;

namespace Shared.Architecture;

public static class TestResultExtensions
{
    
    
    internal static void ShouldBeSuccessful(this TestResult testResult)
    {
        testResult.FailingTypes?.Should().BeEmpty();
    }
    
}