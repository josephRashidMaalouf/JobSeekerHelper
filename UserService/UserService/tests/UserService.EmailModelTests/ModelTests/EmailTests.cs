using UserService.Domain.Exceptions;
using UserService.Domain.Models;

namespace UserService.EmailModelTests.ModelTests;

public class EmailTests
{
    [Theory]
    [InlineData("two@x.xx")]
    [InlineData("three@test.xxx")]
    [InlineData("four@test.xxxx")]
    [InlineData("five@test.xxxxx")]
    [InlineData("testwithnumber1234@x.xx")]
    [InlineData("test.with.sepa-rat_ors@x.xx")]
    [InlineData("test_with-separators.and-numbers1234@x.xx")]
    [InlineData("subdomain@test.sub.com")]
    public void SetEmailValue_ValidFormat_ShouldSetValue(string email)
    {
        //Arrange & Act
        var sut = new Email(email);
        
        //Assert
        Assert.Equal(sut.Value, email);
    }

    [Theory]
    [InlineData("")]
    [InlineData("justSomeText")]
    [InlineData("test@test")]
    [InlineData("x@x.x")]
    [InlineData("forbiddenchars#¤!%£@email.com")]
    [InlineData("two@a@a.com")]
    [InlineData("@email.com")]
    [InlineData("test   @email.com")]
    [InlineData("test@em   ail.com")]
    [InlineData("test@   email.com")]
    [InlineData("test@email.c   om")]
    [InlineData("user@domain,com")]
    [InlineData("user@domain..com")] 
    public void SetEmailValue_InvalidFormat_ShouldInvalidEmailException(string email)
    {
        //Arrange, act and assert
        
        Assert.Throws<InvalidEmailException>(() => new Email(email));
    }
}