using FluentValidation;

namespace KbAis.Intern.Library.Service.Web.Features.Users.LoginingUser.V1;

public class LoginUserResponseValidator : AbstractValidator<LoginUserEndpoint.LoginUserResponse>
{
    public LoginUserResponseValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Token).NotEmpty();
    }
}
