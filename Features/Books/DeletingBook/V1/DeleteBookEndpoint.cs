using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Books.DeletingBook.V1;

public class DeleteBookEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapDelete($"/books/{{book-id}}/delete", HandleRequestAsync)
            .WithTags("books");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromRoute(Name = "book-id")] Guid bookId,
        [FromServices] DeleteBookCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await commandHandler.HandleAsync(new(bookId), c)
                .MapError(error => Results.BadRequest(error))
                .Map(() => Results.Ok("Deleted successfully")
            );

        return result.IsSuccess ? result.Value : result.Error;
    }
}
