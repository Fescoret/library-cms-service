using FluentValidation;
using KbAis.Intern.Library.Service.Web.Data.Models;

namespace KbAis.Intern.Library.Service.Web.Data.Validators;

public class AuthorValidator: AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.SecondName).NotNull();
    }
}
