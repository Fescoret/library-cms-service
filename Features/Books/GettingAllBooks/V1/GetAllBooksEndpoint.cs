using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Books.GettingAllBooks.V1;

public class GetAllBooksEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/books", HandleRequestAsync)
            .WithTags("books");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromServices] GetAllBooksCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await commandHandler.HandleAsync(c)
            .MapError(error => Results.BadRequest(error))
            .Map(books => Results.Ok(books));

        return result.IsSuccess ? result.Value : result.Error;
    }
}
