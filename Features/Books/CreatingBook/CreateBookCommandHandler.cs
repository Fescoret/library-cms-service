using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Books.CreatingBook;

public record CreateBookCommand(string Title, ICollection<Guid> Authors, string CoverUrl);

public class CreateBookCommandHandler
{
    private readonly IDocumentSession session;

    public CreateBookCommandHandler(IDocumentSession session)
    {
        this.session = session;
    }

    public async Task<Result> HandleAsync(CreateBookCommand command, CancellationToken c)
    {
        var loadAuthors = (await session.LoadManyAsync<Author>(command.Authors)).ToList();
        return await Cover.Create(command.CoverUrl)
                .Map(cover => Book.Create(command.Title, loadAuthors, cover)
                    .Tap(() => session.Store(cover))
                    .Tap(book => session.Store(book))
                    .Tap(() => session.SaveChangesAsync(c)));
            
    }
}
