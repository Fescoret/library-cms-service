using FluentValidation;

namespace KbAis.Intern.Library.Service.Web.Features.Authors.CreateAuthor.V1;

public class CreateAuthorRequestValidator : AbstractValidator<CreateAuthorEndpoint.CreateAuthorRequest>
{
    public CreateAuthorRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.SecondName).NotNull();
    }
}
