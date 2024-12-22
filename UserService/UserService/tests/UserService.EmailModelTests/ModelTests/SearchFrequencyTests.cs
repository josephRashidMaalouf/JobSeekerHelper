using FakeItEasy;
using UserService.Domain.Enums;
using UserService.Domain.Exceptions;
using UserService.Domain.Models;

namespace UserService.EmailModelTests.ModelTests;

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
}