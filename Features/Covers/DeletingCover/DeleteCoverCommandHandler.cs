using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Covers.DeletingCover;

public record DeleteCoverCommand(Guid CoverId);

public class DeleteCoverCommandHandler
{
    private readonly IDocumentSession session;

    public DeleteCoverCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result> HandleAsync(DeleteCoverCommand command, CancellationToken c)
    {
        return await session.Load<Author>(command.CoverId).AsMaybe()
            .ToResult("Cover with such ID does not exist")
            .Tap(cover => session.Delete(cover))
            .Tap(() => session.SaveChangesAsync(c));
    }
}
