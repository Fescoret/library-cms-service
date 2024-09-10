using FluentValidation;
using KbAis.Intern.Library.Service.Web.Data.Models;

namespace KbAis.Intern.Library.Service.Web.Data.Validators;

public class BookValidator: AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Authors).NotNull();
        RuleFor(x => x.Cover).NotNull();
        RuleForEach(x => x.Authors)
            .ChildRules(c =>
            {
                c.RuleFor(a => a.Id).NotNull();
                c.RuleFor(a => a.FirstName).NotEmpty();
                c.RuleFor(a => a.SecondName).NotNull();
            });
    }
}
