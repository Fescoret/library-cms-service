using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Authors.GettingAllAuthors;

public class GetAllAuthorsCommandHandler
{
    private readonly IQuerySession session;

    public GetAllAuthorsCommandHandler(IQuerySession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result<IReadOnlyList<Author>>> HandleAsync(CancellationToken c)
    {
        return (await session.Query<Author>().ToListAsync(c)).AsMaybe()
            .ToResult("Server Error: Authors table does'nt exist")
            .Bind(users => users.IsEmpty()
                        ? Result.Failure<IReadOnlyList<Author>>("There is no authors")
                        : Result.Success(users));
    }
}
