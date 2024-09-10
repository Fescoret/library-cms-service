using FluentValidation;

namespace KbAis.Intern.Library.Service.Web.Features.Covers.CreatingCover.V1;

public class CreateCoverRequestValidator : AbstractValidator<CreateCoverEndpoint.CreateCoverRequest>
{
    public CreateCoverRequestValidator()
    {
        RuleFor(x => x.CoverImageHttpLink).NotEmpty();
    }
}
