using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Covers.CreatingCover;

public record CreateCoverCommand(string CoverImageHttpLink);

public class CreateCoverCommandHandler
{
    private readonly IDocumentSession session;

    public CreateCoverCommandHandler(IDocumentSession session)
    {
        this.session = session;
    }

    public async Task<Result> HandleAsync(CreateCoverCommand command, CancellationToken c)
    {
        return await Cover.Create(command.CoverImageHttpLink)
                .Tap(cover => session.Store(cover))
                .Tap(() => session.SaveChangesAsync(c));
    }
}
