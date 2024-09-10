using FluentValidation;

namespace KbAis.Intern.Library.Service.Web.Features.Users.RegisteringUser.V1;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserEndpoint.RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
