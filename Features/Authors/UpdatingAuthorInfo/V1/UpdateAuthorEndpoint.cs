using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Utils.FluentValidatorEx;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Authors.UpdateAuthor.V1;

public class UpdateAuthorEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapPut($"/authors/{{author-id}}/update", HandleRequestAsync)
            .WithTags("authors");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromRoute(Name = "author-id")] Guid authorId,
        [FromBody] UpdateAuthorRequest request,
        [FromServices] UpdateAuthorRequestValidator validator,
        [FromServices] UpdateAuthorCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await validator.ValidateForResult(request)
            .MapError(validationError =>
                Results.BadRequest(validationError.ToDictionary())
            )
            .Bind(x => commandHandler.HandleAsync(new(authorId, x.FirstName, x.SecondName), c)
                .MapError(error => Results.BadRequest(error))
                .Map(() => Results.Ok("Updated successfully"))
            );

        return result.IsSuccess ? result.Value : result.Error;
    }

    public class UpdateAuthorRequest
    {
        public string FirstName { get; init; } = null!;

        public string? SecondName { get; init; }
    }
}
