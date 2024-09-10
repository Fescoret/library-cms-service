using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Utils.FluentValidatorEx;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Books.UpdatingBookInfo.V1;

public class UpdateBookEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapPut($"/books/{{book-id}}/update", HandleRequestAsync)
            .WithTags("books");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromRoute(Name = "book-id")] Guid bookId,
        [FromBody] UpdateBookRequest request,
        [FromServices] UpdateBookRequestValidator validator,
        [FromServices] UpdateBookCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await validator.ValidateForResult(request)
            .MapError(validationError =>
                Results.BadRequest(validationError.ToDictionary())
            )
            .Bind(x => commandHandler.HandleAsync(new(bookId, x.Title, x.Authors, x.CoverUrl), c)
                .MapError(error => Results.BadRequest(error))
                .Map(() => Results.Ok("Updated successfully"))
            );

        return result.IsSuccess ? result.Value : result.Error;
    }

    public class UpdateBookRequest
    {
        public string Title { get; init; } = null!;

        public ICollection<Guid> Authors { get; init; } = null!;

        public string CoverUrl { get; init; } = null!;
    }
}
