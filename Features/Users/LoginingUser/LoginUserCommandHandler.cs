using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;
using static KbAis.Intern.Library.Service.Web.Features.Users.LoginingUser.V1.LoginUserEndpoint;

namespace KbAis.Intern.Library.Service.Web.Features.Users.LoginingUser;

public record LoginUserCommand(string EmailAddress, string Password);

public class LoginUserCommandHandler
{
    private readonly IQuerySession session;

    public LoginUserCommandHandler(IQuerySession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result<LoginUserResponse>> HandleAsync(LoginUserCommand command, CancellationToken c)
    {
        return (await session.Query<User>()
            .Where(x => x.Email == command.EmailAddress)
            .FirstOrDefaultAsync(c)).AsMaybe()
            .ToResult("User with such Email does not exist")
            .Bind(user =>
            {
                return user.Login(command.Password)
                    ? Result.Success(user)
                    : Result.Failure<User>("Incorrect password");
            })
            .Map(user => 
            { 
                LoginUserResponse response = new() { Email = command.EmailAddress, Token = user.CreateToken() };
                return response;
            });
    }
}
