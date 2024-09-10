using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Covers.GettingAllCovers;

public class GetAllCoversCommandHandler
{
    private readonly IQuerySession session;

    public GetAllCoversCommandHandler(IQuerySession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result<IReadOnlyList<Cover>>> HandleAsync(CancellationToken c)
    {
        return (await session.Query<Cover>().ToListAsync(c)).AsMaybe()
            .ToResult("Server Error: Covers table does'nt exist")
            .Bind(covers => covers.IsEmpty()
                        ? Result.Failure<IReadOnlyList<Cover>>("There is no covers")
                        : Result.Success(covers));
    }
}
