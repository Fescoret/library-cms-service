using Microsoft.AspNetCore.Mvc;
using KbAis.Intern.Library.Service.Web.Utils.FluentValidatorEx;
using CSharpFunctionalExtensions;

namespace KbAis.Intern.Library.Service.Web.Features.Users.UpdatingUserInfo.V1;

public class UpdateUserEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapPut($"/users/{{user-id}}/update", HandleRequestAsync)
            .WithTags("users");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromRoute(Name = "user-id")] Guid userId,
        [FromBody] UpdateUserRequest request,
        [FromServices] UpdateUserRequestValidator validator,
        [FromServices] UpdateUserCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await validator.ValidateForResult(request)
            .MapError(validationError =>
                Results.BadRequest(validationError.ToDictionary())
            )
            .Bind(x => commandHandler.HandleAsync(new(userId, x.Email, x.Password, x.FirstName, x.LastName), c)
                .MapError(error => Results.BadRequest(error))
                .Map(() => Results.Ok("Updated successfully"))
            );

        return result.IsSuccess
            ? result.Value
            : result.Error;
    }

    public class UpdateUserRequest
    {
        public string FirstName { get; init; } = null!;

        public string LastName { get; init; } = null!;

        public string Email { get; init; } = null!;

        public string Password { get; init; } = null!;
    }
}
