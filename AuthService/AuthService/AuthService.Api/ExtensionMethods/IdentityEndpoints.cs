using System.Security.Claims;
using AuthService.Api.Dtos;
using AuthService.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.ExtensionMethods;

public static class IdentityEndpoints
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/logout", LogoutAsync).RequireAuthorization();
        app.MapGet("/roles", GetRolesAsync).RequireAuthorization();
        app.MapPost("/register-as-user", RegisterAsUserAsync);
        app.MapDelete("/delete", DeleteAsync).RequireAuthorization();

        return app;
    }

    private static async Task<IResult> LogoutAsync(SignInManager<User> signInManager, [FromBody] object? empty)
    {
        if (empty is null)
            return Results.Unauthorized();

        await signInManager.SignOutAsync();

        return Results.Ok();
    }

    private static async Task<IResult> GetRolesAsync(IHttpContextAccessor httpContextAccessor,
        UserManager<User> userManager)
    {
        var contextUser = httpContextAccessor.HttpContext?.User;

        if (contextUser is null)
        {
            return Results.Unauthorized();
        }

        if (contextUser.Identity is null || !contextUser.Identity.IsAuthenticated)
        {
            return Results.Unauthorized();
        }


        var identity = (ClaimsIdentity)contextUser.Identity;
        var username = identity.Name ?? "";

        var user = await userManager.FindByNameAsync(username);

        if (user is null)
        {
            return Results.Unauthorized();
        }

        var roles = await userManager.GetRolesAsync(user);

        return TypedResults.Json(roles);
    }

    private static async Task<IResult> RegisterAsUserAsync(RegisterDto registerDto, UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        var user = new User { UserName = registerDto.UserName, Email = registerDto.UserName };
        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.Errors);
        }

        var roleExist = await roleManager.RoleExistsAsync("user");
        if (!roleExist)
        {
            await roleManager.CreateAsync(new Role("user"));
        }

        await userManager.AddToRoleAsync(user, "user");

        return Results.Ok("User registered successfully with 'user' role.");
    }
    
    private static async Task<IResult> DeleteAsync(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {

        var httpContextUser = httpContextAccessor.HttpContext?.User;

        if (httpContextUser is null)
        {
            return Results.Unauthorized();
        }
        
        if (httpContextUser.Identity is null || !httpContextUser.Identity.IsAuthenticated)
        {
            return Results.Unauthorized();
        }
        var email = httpContextUser.Identity.Name;

        if (email is null)
        {
            return Results.Unauthorized();
        }
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Results.Unauthorized();
        }
        await userManager.DeleteAsync(user);
        return Results.Ok();

    }
}