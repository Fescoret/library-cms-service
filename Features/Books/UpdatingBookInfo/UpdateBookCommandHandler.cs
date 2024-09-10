using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Books.UpdatingBookInfo;

public record UpdateBookCommand(Guid BookId, string Title, ICollection<Guid> Authors, string CoverUrl);

public class UpdateBookCommandHandler
{
    private readonly IDocumentSession session;

    public UpdateBookCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result> HandleAsync(UpdateBookCommand command, CancellationToken c)
    {
        var loadAuthors = session.LoadMany<Author>(command.Authors).ToList();
        return await session.Load<Book>(command.BookId).AsMaybe()
            .ToResult("Book with such ID does not exist")
            .Bind(book => book.Cover.UpdateCover(session, command.CoverUrl)
                .Tap(cover => session.Update(cover))
                .Map(cover => book.UpdateBook(command.Title, loadAuthors, cover)))
            .Tap(book => session.Update(book))
            .Tap(() => session.SaveChangesAsync(c));
    }
}
