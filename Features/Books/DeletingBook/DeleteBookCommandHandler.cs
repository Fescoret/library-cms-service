using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Books.DeletingBook;

public record DeleteBookCommand(Guid BookId);

public class DeleteBookCommandHandler
{
    private readonly IDocumentSession session;

    public DeleteBookCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result> HandleAsync(DeleteBookCommand command, CancellationToken c)
    {
        return await session.Load<Book>(command.BookId).AsMaybe()
            .ToResult("Book with such ID does not exist")
            .Bind(book => book.DeleteBook(session))
            .Tap(book => session.Delete(book))
            .Tap(() => session.SaveChangesAsync(c));
    }
}
