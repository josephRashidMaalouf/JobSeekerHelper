using FakeItEasy;
using SearchService.Domain.Enums;
using SearchService.Domain.Exceptions;
using SearchService.Domain.Models;

namespace SearchService.UnitTests.ModelTests;

public class SearchFrequencyTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void SetFrequency_ValueIsLessThanOne_ShouldThrowInvalidFrequencyException(int value)
    {
        //Arrange, act and assert
        Assert.Throws<InvalidFrequencyException>(() => new SearchFrequency()
        {
            Frequency = value,
            Interval = A.Dummy<IntervalOption>()
        });
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void SetFrequency_ValueIsOneOrMore_ShouldSetFrequency(int value)
    {
        //Arrange and act
        var result = new SearchFrequency
        {
            Frequency = value, 
            Interval = A.Dummy<IntervalOption>()
        };
        
        Assert.Equal(value, result.Frequency);
    }
}