using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Utils.FluentValidatorEx;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Authors.CreateAuthor.V1;

public class CreateAuthorEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/authors/create", HandleRequestAsync)
            .WithTags("authors");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromServices] CreateAuthorCommandHandler commandHandler,
        [FromServices] CreateAuthorRequestValidator validator,
        [FromBody] CreateAuthorRequest request,
        CancellationToken c
    )
    {
        var result = await validator.ValidateForResult(request)
            .MapError(validationResult => 
                Results.BadRequest(validationResult.ToDictionary()))
            .Bind(x => commandHandler.HandleAsync(new(request.FirstName, request.SecondName), c)
                .MapError(error => Results.BadRequest(error))
                .Map(() => Results.Created("/admin-panel/authors", null))
            );

        return result.IsSuccess ? result.Value : result.Error;
    }

    public class CreateAuthorRequest
    {
        public string FirstName { get; init; } = null!;

        public string? SecondName { get; init; }
    }
}
