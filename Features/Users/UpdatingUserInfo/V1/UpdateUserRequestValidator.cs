using FluentValidation;

namespace KbAis.Intern.Library.Service.Web.Features.Users.UpdatingUserInfo.V1;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserEndpoint.UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
