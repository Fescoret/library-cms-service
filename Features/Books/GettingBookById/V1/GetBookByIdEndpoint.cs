using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Books.GettingBookById.V1;

public class GetBookByIdEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapGet($"/books/{{book-id}}", HandleRequestAsync)
            .WithTags("books");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromRoute(Name = "book-id")] Guid bookId,
        [FromServices] GetBookByIdCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await commandHandler.HandleAsync(new(bookId), c)
                .MapError(error => Results.BadRequest(error))
                .Map(book => Results.Ok(book));

        return result.IsSuccess ? result.Value : result.Error;
    }
}
