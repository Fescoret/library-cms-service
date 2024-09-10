using FluentValidation;

namespace KbAis.Intern.Library.Service.Web.Features.Authors.UpdateAuthor.V1;

public class UpdateAuthorRequestValidator : AbstractValidator<UpdateAuthorEndpoint.UpdateAuthorRequest>
{
    public UpdateAuthorRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.SecondName).NotNull();
    }
}
