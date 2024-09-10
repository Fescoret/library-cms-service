using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Authors.CreateAuthor;

public record CreateAuthorCommand(string FirstName, string? SecondName);

public class CreateAuthorCommandHandler
{
    private readonly IDocumentSession session;

    public CreateAuthorCommandHandler(IDocumentSession session)
    {
        this.session = session;
    }

    public async Task<Result> HandleAsync(CreateAuthorCommand command, CancellationToken c)
    {
        return await Author.Create(command.FirstName, command.SecondName)
                .Tap(author => session.Store(author))
                .Tap(() => session.SaveChangesAsync(c));
    }
}
