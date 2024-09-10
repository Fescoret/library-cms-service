using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Covers.UpdatingCoverInfo;

public record UpdateCoverCommand(Guid CoverId, string CoverImageHttpLink);

public class UpdateCoverCommandHandler
{
    private readonly IDocumentSession session;

    public UpdateCoverCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result> HandleAsync(UpdateCoverCommand command, CancellationToken c)
    {
        return await session.Load<Cover>(command.CoverId).AsMaybe()
            .ToResult("Cover with such ID does not exist")
            .Bind(cover => cover.UpdateCover(session, command.CoverImageHttpLink))
            .Tap(cover => session.Update(cover))
            .Tap(() => session.SaveChangesAsync(c));
    }
}
