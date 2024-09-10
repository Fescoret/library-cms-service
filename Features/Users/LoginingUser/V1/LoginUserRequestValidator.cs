using FluentValidation;

namespace KbAis.Intern.Library.Service.Web.Features.Users.LoginingUser.V1;

public class LoginUserRequestValidator : AbstractValidator<LoginUserEndpoint.LoginUserRequest>
{
    LoginUserRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
