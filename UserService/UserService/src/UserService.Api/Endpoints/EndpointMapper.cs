namespace UserService.Api.Endpoints;

public static class EndpointMapper
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapResumeEndpoints();

        return app;
    }
}