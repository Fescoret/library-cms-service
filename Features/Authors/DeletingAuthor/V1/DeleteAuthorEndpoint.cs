using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Authors.DeletingAuthor.V1;

public class DeleteAuthorEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapDelete($"/authors/{{author-id}}/delete", HandleRequestAsync)
            .WithTags("authors");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromRoute(Name = "author-id")] Guid authorId,
        [FromServices] DeleteAuthorCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await commandHandler.HandleAsync(new(authorId), c)
                .MapError(error => Results.BadRequest(error))
                .Map(() => Results.Ok("Deleted successfully")
            );

        return result.IsSuccess ? result.Value : result.Error;
    }
}
