using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Utils.FluentValidatorEx;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace KbAis.Intern.Library.Service.Web.Features.Users.LoginingUser.V1;

public class LoginUserEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapPut("/auth/signup", HandleRequestAsync)
            .WithTags("users");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromBody] LoginUserRequest request,
        [FromServices] LoginUserRequestValidator reqValidator,
        [FromServices] LoginUserResponseValidator resValidator,
        [FromServices] LoginUserCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await reqValidator.ValidateForResult(request)
            .MapError(validationError =>
                Results.BadRequest(validationError.ToDictionary())
            )
            .Bind(x => commandHandler.HandleAsync(new(x.Email, x.Password), c)
                .MapError(error => Results.BadRequest(error))
                .Map(async response => await resValidator.ValidateForResult(response)
                    .MapError(error => Results.Problem("Response isn't valid"))
                    .Map(response => Results.Ok(response)))
            );

        return result.IsSuccess
            ? result.Value.IsSuccess
                ? result.Value.Value
                : result.Value.Error
            : result.Error;
    }

    public class LoginUserRequest
    {
        public string Email { get; init; } = null!;

        public string Password { get; init; } = null!;
    }

    public class LoginUserResponse
    {
        public string Email { get; init; } = null!;

        public string Token { get; init; } = null!;
    }
}
