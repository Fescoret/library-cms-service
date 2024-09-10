using FluentValidation;

namespace KbAis.Intern.Library.Service.Web.Features.Books.UpdatingBookInfo.V1;

public class UpdateBookRequestValidator : AbstractValidator<UpdateBookEndpoint.UpdateBookRequest>
{
    public UpdateBookRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Authors).NotNull();
        RuleFor(x => x.CoverUrl).NotEmpty();
    }
}
