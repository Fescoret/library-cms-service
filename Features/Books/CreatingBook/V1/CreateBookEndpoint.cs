using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Utils.FluentValidatorEx;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Books.CreatingBook.V1;

public class CreateBookEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/books/create", HandleRequestAsync)
            .WithTags("books");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromServices] CreateBookCommandHandler commandHandler,
        [FromServices] CreateBookRequestValidator validator,
        [FromBody] CreateBookRequest request,
        CancellationToken c
    )
    {
        var result = await validator.ValidateForResult(request)
            .MapError(validationResult =>
                Results.BadRequest(validationResult.ToDictionary()))
            .Bind(x => commandHandler.HandleAsync(new(request.Title, request.Authors, request.CoverUrl), c)
                .MapError(error => Results.BadRequest(error))
                .Map(() => Results.Created("/admin-panel/books", null))
            );

        return result.IsSuccess ? result.Value : result.Error;
    }

    public class CreateBookRequest
    {
        public string Title { get; init; } = null!;

        public ICollection<Guid> Authors { get; init; } = null!;

        public string CoverUrl { get; init; } = null!;
    }
}
