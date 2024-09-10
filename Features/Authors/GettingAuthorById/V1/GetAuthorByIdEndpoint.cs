using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Authors.GettingAuthorById.V1;

public class GetAuthorByIdEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapGet($"/authors/{{author-id}}", HandleRequestAsync)
            .WithTags("authors");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromRoute(Name = "author-id")] Guid authorId,
        [FromServices] GetAuthorByIdCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await commandHandler.HandleAsync(new(authorId), c)
                .MapError(error => Results.BadRequest(error))
                .Map(user => Results.Ok(user));

        return result.IsSuccess ? result.Value : result.Error;
    }
}
