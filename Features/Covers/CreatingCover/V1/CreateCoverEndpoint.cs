using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Utils.FluentValidatorEx;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Covers.CreatingCover.V1;

public class CreateCoverEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/covers/create", HandleRequestAsync)
            .WithTags("covers");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromServices] CreateCoverCommandHandler commandHandler,
        [FromServices] CreateCoverRequestValidator validator,
        [FromBody] CreateCoverRequest request,
        CancellationToken c
    )
    {
        var result = await validator.ValidateForResult(request)
            .MapError(validationResult =>
                Results.BadRequest(validationResult.ToDictionary()))
            .Bind(x => commandHandler.HandleAsync(new(request.CoverImageHttpLink), c)
                .MapError(error => Results.BadRequest(error))
                .Map(() => Results.Created("/admin-panel/covers", null))
            );

        return result.IsSuccess ? result.Value : result.Error;
    }

    public class CreateCoverRequest
    {
        public string CoverImageHttpLink { get; init; } = null!;
    }
}
