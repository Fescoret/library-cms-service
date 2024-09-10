using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Covers.GettingCoverById.V1;

public class GetCoverByIdEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapGet($"/covers/{{cover-id}}", HandleRequestAsync)
            .WithTags("covers");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromRoute(Name = "cover-id")] Guid coverId,
        [FromServices] GetCoverByIdCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await commandHandler.HandleAsync(new(coverId), c)
                .MapError(error => Results.BadRequest(error))
                .Map(cover => Results.Ok(cover));

        return result.IsSuccess ? result.Value : result.Error;
    }
}
