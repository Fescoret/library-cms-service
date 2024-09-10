using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Authors.DeletingAuthor;

public record DeleteAuthorCommand(Guid AuthorId);

public class DeleteAuthorCommandHandler
{
    private readonly IDocumentSession session;

    public DeleteAuthorCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result> HandleAsync(DeleteAuthorCommand command, CancellationToken c)
    {
        return await session.Load<Author>(command.AuthorId).AsMaybe()
            .ToResult("Author with such ID does not exist")
            .Tap(author => session.Delete(author))
            .Tap(() => session.SaveChangesAsync(c));
    }
}
