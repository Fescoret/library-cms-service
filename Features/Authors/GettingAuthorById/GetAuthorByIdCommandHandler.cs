using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Authors.GettingAuthorById;

public record GetAuthorByIdCommand(Guid AuthorId);

public class GetAuthorByIdCommandHandler
{
    private readonly IDocumentSession session;

    public GetAuthorByIdCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result<Author>> HandleAsync(GetAuthorByIdCommand command, CancellationToken c)
    {
        return (await session.LoadAsync<Author>(command.AuthorId, c)).AsMaybe()
            .ToResult("Author with such ID does not exist");
    }
}
