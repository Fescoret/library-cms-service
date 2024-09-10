using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Authors.GettingAllAuthors.V1;

public class GetAllAuthorsEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/authors", HandleRequestAsync)
            .WithTags("authors");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromServices] GetAllAuthorsCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await commandHandler.HandleAsync(c)
            .MapError(error => Results.BadRequest(error))
            .Map(users => Results.Ok(users));

        return result.IsSuccess ? result.Value : result.Error;
    }
}
