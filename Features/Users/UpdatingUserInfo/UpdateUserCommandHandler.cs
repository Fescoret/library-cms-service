using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Users.UpdatingUserInfo;

public record UpdateUserCommand(Guid UserId, string EmailAddress, string Password, string FirstName, string LastName);

public class UpdateUserCommandHandler
{
    private readonly IDocumentSession session;

	public UpdateUserCommandHandler(IDocumentSession documentSession)
	{
		this.session = documentSession;
	}

	public async Task<Result> HandleAsync(UpdateUserCommand command, CancellationToken c)
	{
		return await session.Load<User>(command.UserId).AsMaybe()
			.ToResult("User with such ID does not exist")
			.Bind(user => user.UpdateUser(command.EmailAddress, command.Password, command.FirstName, command.LastName))
			.Tap(user => session.Update(user))
			.Tap(() => session.SaveChangesAsync(c));
	}
}
