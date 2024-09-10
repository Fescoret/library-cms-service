using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Users.RegisteringUser;

public record RegisterUserCommand(string EmailAddress, string Password, string FirstName, string LastName);

public class RegisterUserCommandHandler
{
    private readonly IDocumentSession session;

    public RegisterUserCommandHandler(IDocumentSession session) {
        this.session = session;
    }

    public async Task<Result> HandleAsync(RegisterUserCommand command, CancellationToken c) {
        return await User.Create(command.EmailAddress, command.Password, command.FirstName, command.LastName)
            .Tap(user => session.Store(user))
            .Tap(() => session.SaveChangesAsync(c));
    }
}
