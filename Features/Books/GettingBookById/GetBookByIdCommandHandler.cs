using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Books.GettingBookById;

public record GetBookByIdCommand(Guid BookId);

public class GetBookByIdCommandHandler
{
    private readonly IDocumentSession session;

    public GetBookByIdCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result<Book>> HandleAsync(GetBookByIdCommand command, CancellationToken c)
    {
        return (await session.LoadAsync<Book>(command.BookId, c)).AsMaybe()
            .ToResult("Book with such ID does not exist");
    }
}
