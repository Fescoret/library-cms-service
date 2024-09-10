using Baseline;
using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Authors.UpdateAuthor;

public record UpdateAuthorCommand(Guid AuthorId, string FirstName, string? SecondName);

public class UpdateAuthorCommandHandler
{
    private readonly IDocumentSession session;

    public UpdateAuthorCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    private static void BooksUpdate(IDocumentSession session, List<Book> books, Guid authorId)
    {
        var author = session.Load<Author>(authorId);
        books.Each(book =>
        {
            var authors = book.Authors.ToList();
            authors[authors.FindIndex(x => x.Id == authorId)] = author;
            book.UpdateBook(book.Title, authors, book.Cover);
            session.Update(book);
        });
        //session.SaveChanges();
    }

    public async Task<Result> HandleAsync(UpdateAuthorCommand command, CancellationToken c)
    {
        return await session.Load<Author>(command.AuthorId).AsMaybe()
            .ToResult("Author with such ID does not exist")
            .Bind(author => author.UpdateAuthor(command.FirstName, command.SecondName))
            .Tap(author => session.Update(author))
            .Map(author =>
            {
                return session.Query<Book>()
                    .Where(book => book.Authors.Where(a => a.Id == author.Id).Any())
                    .ToList();
            })
            .TapIf(books => books.Any(), books => BooksUpdate(session, books, command.AuthorId))
            .Tap(() => session.SaveChangesAsync(c));
    }
}
