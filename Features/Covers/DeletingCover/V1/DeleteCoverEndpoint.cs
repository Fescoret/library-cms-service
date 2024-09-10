using Microsoft.AspNetCore.Mvc;
using CSharpFunctionalExtensions;

namespace KbAis.Intern.Library.Service.Web.Features.Covers.DeletingCover.V1;

public class DeleteCoverEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapDelete($"/covers/{{cover-id}}/delete", HandleRequestAsync)
            .WithTags("covers");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromRoute(Name = "cover-id")] Guid coverId,
        [FromServices] DeleteCoverCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await commandHandler.HandleAsync(new(coverId), c)
                .MapError(error => Results.BadRequest(error))
                .Map(() => Results.Ok("Deleted successfully")
            );

        return result.IsSuccess ? result.Value : result.Error;
    }
}
