using Microsoft.AspNetCore.Mvc;
using CSharpFunctionalExtensions;

namespace KbAis.Intern.Library.Service.Web.Features.Users.GetingUserById.V1;

public class GetUserByIdEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapGet($"/users/{{user-id}}", HandleRequestAsync)
            .WithTags("users");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromRoute(Name = "user-id")] Guid userId,
        [FromServices] GetUserByIdCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await commandHandler.HandleAsync(new(userId), c)
                .MapError(error => Results.BadRequest(error))
                .Map(user => Results.Ok(user));

        return result.IsSuccess
            ? result.Value
            : result.Error;
    }
}
