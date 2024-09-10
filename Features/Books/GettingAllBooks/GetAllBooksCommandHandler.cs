using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Books.GettingAllBooks;

public class GetAllBooksCommandHandler
{
    private readonly IQuerySession session;

    public GetAllBooksCommandHandler(IQuerySession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result<IReadOnlyList<Book>>> HandleAsync(CancellationToken c)
    {
        return (await session.Query<Book>().ToListAsync(c)).AsMaybe()
            .ToResult("Server Error: Books table does'nt exist")
            .Bind(books => books.IsEmpty()
                        ? Result.Failure<IReadOnlyList<Book>>("There is no books")
                        : Result.Success(books));
    }
}
