using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Covers.GettingAllCovers.V1;

public class GetAllCoversEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/covers", HandleRequestAsync)
            .WithTags("covers");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromServices] GetAllCoversCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await commandHandler.HandleAsync(c)
            .MapError(error => Results.BadRequest(error))
            .Map(covers => Results.Ok(covers));

        return result.IsSuccess ? result.Value : result.Error;
    }
}
