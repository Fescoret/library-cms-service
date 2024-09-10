using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Utils.FluentValidatorEx;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Covers.UpdatingCoverInfo.V1;

public class UpdateCoverEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapPut($"/covers/{{cover-id}}/update", HandleRequestAsync)
            .WithTags("covers");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromRoute(Name = "cover-id")] Guid coverId,
        [FromBody] UpdateCoverRequest request,
        [FromServices] UpdateCoverRequestValidator validator,
        [FromServices] UpdateCoverCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await validator.ValidateForResult(request)
            .MapError(validationError =>
                Results.BadRequest(validationError.ToDictionary())
            )
            .Bind(x => commandHandler.HandleAsync(new(coverId, x.CoverImageHttpLink), c)
                .MapError(error => Results.BadRequest(error))
                .Map(() => Results.Ok("Updated successfully"))
            );

        return result.IsSuccess ? result.Value : result.Error;
    }

    public class UpdateCoverRequest
    {
        public string CoverImageHttpLink { get; init; } = null!;
    }
}
