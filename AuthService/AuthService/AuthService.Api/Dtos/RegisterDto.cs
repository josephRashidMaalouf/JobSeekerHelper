namespace AuthService.Api.Dtos;

public class RegisterDto
{
    public required string UserName { get; init; }
    public required string Password { get; init; }
}