using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Covers.GettingCoverById;

public record GetCoverByIdCommand(Guid CoverId);

public class GetCoverByIdCommandHandler
{
    private readonly IDocumentSession session;

    public GetCoverByIdCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result<Cover>> HandleAsync(GetCoverByIdCommand command, CancellationToken c)
    {
        return (await session.LoadAsync<Cover>(command.CoverId, c)).AsMaybe()
            .ToResult("Cover with such ID does not exist");
    }
}
