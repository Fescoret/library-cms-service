using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using KbAis.Intern.Library.Service.Web.Features.Users.UpdatingUserInfo;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Users.GetingUserById;

public record GetUserByIdCommand(Guid UserId);

public class GetUserByIdCommandHandler
{
    private readonly IDocumentSession session;

    public GetUserByIdCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result<User>> HandleAsync(GetUserByIdCommand command, CancellationToken c)
    {
        return (await session.LoadAsync<User>(command.UserId, c)).AsMaybe()
            .ToResult("User with such ID does not exist");
    }
}
