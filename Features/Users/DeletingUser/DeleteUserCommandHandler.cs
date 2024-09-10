using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Users.DeletingUser;

public record DeleteUserCommand(Guid UserId);

public class DeleteUserCommandHandler
{
    private readonly IDocumentSession session;

    public DeleteUserCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result> HandleAsync(DeleteUserCommand command, CancellationToken c)
    {
        return await session.Load<User>(command.UserId).AsMaybe()
            .ToResult("User with such ID does not exist")
            .Bind(user => user.Orders.Any()
                ? Result.Failure<User>("The user cannot be deleted while he has active orders")
                : Result.Success(user))
            .Tap(user => session.Delete(user))
            .Tap(() => session.SaveChangesAsync(c));
    }
}
