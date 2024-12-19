using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.Entities;

public class Role : IdentityRole<Guid>
{
    public Role()
    {
        
    }

    public Role(string roleName) : base(roleName)
    {
        
    }

    public virtual ICollection<UserRole> UserRoles { get; init; } = new List<UserRole>();
}