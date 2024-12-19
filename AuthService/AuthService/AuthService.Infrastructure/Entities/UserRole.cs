using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.Entities;

public class UserRole : IdentityUserRole<Guid>
{
    public virtual required User User { get; init; }
    public virtual required Role Role { get; init; }
    
}