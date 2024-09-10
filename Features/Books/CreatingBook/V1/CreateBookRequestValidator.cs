using FluentValidation;

namespace KbAis.Intern.Library.Service.Web.Features.Books.CreatingBook.V1;

public class CreateBookRequestValidator : AbstractValidator<CreateBookEndpoint.CreateBookRequest>
{
    public CreateBookRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Authors).NotNull();
        RuleFor(x => x.CoverUrl).NotEmpty();
    }
}
