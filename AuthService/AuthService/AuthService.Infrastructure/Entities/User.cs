using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.Entities;

public class User : IdentityUser<Guid>
{
    public virtual ICollection<IdentityUserClaim<Guid>>? Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<Guid>>? Logins { get; set; }
    public virtual ICollection<IdentityUserToken<Guid>>? Tokens { get; set; }
    public virtual ICollection<UserRole>? UserRoles { get; set; }
}