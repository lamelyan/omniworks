using OmniWorks.Application.Users;
using OmniWorks.Extensions;
using OmniWorks.SharedKernal;
using Wolverine;

namespace OmniWorks.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users", async (CreateUserCommand command, IMessageBus bus) =>
        {
            var result = await bus.InvokeAsync<Result<Guid>>(command);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        });
    }
}
