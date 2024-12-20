using System.Security.Claims;
using AuthService.Api.Dtos;
using AuthService.Infrastructure.Entities;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.ExtensionMethods.Endpoints;

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

    private static async Task<IResult> LogoutAsync(SignInManager<User> signInManager, [FromBody] object? empty, ILogger<Program> logger)
    {
        if (empty is null)
        {
            logger.LogWarning("No empty object provided in body", empty);
            return Results.Unauthorized();
        }
        await signInManager.SignOutAsync();

        return Results.Ok();
    }

    private static async Task<IResult> GetRolesAsync(IHttpContextAccessor httpContextAccessor,
        UserManager<User> userManager, ILogger<Program> logger)
    {
        var contextUser = httpContextAccessor.HttpContext?.User;

        if (contextUser is null)
        {
            logger.LogWarning("No context user provided", contextUser);
            return Results.Unauthorized();
        }

        if (contextUser.Identity is null || !contextUser.Identity.IsAuthenticated)
        {
            logger.LogWarning("Context user is unauthorized", contextUser);
            return Results.Unauthorized();
        }


        var identity = (ClaimsIdentity)contextUser.Identity;
        var username = identity.Name ?? "";

        var user = await userManager.FindByNameAsync(username);

        if (user is null)
        {
            logger.LogWarning("Username not found in database", user);
            return Results.Unauthorized();
        }

        var roles = await userManager.GetRolesAsync(user);

        return TypedResults.Json(roles);
    }

    private static async Task<IResult> RegisterAsUserAsync(RegisterDto registerDto, UserManager<User> userManager,
        RoleManager<Role> roleManager, ILogger<Program> logger)
    {
        var user = new User { UserName = registerDto.UserName, Email = registerDto.UserName };
        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            logger.LogWarning("Failed to create user: {username}", registerDto.UserName);
            result.Errors.ToList().ForEach(error => logger.LogError(error.Code, error.Description));
            return Results.BadRequest(result.Errors);
        }

        var roleExist = await roleManager.RoleExistsAsync("user");
        if (!roleExist)
        {
            await roleManager.CreateAsync(new Role("user"));
        }

        await userManager.AddToRoleAsync(user, "user");
        
        logger.LogInformation("User created a new account with username: {username}", registerDto.UserName);
        return Results.Ok("User registered successfully with 'user' role.");
    }
    
    private static async Task<IResult> DeleteAsync(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, ILogger<Program> logger)
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
        
        logger.LogInformation("User deleted an account with email: {email}", email);
        return Results.Ok();

    }
}