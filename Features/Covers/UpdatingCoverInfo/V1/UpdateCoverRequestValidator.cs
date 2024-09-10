using FluentValidation;

namespace KbAis.Intern.Library.Service.Web.Features.Covers.UpdatingCoverInfo.V1;

public class UpdateCoverRequestValidator : AbstractValidator<UpdateCoverEndpoint.UpdateCoverRequest>
{
    public UpdateCoverRequestValidator()
    {
        RuleFor(x => x.CoverImageHttpLink).NotEmpty();
    }
}
