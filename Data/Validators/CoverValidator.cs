using FluentValidation;
using KbAis.Intern.Library.Service.Web.Data.Models;

namespace KbAis.Intern.Library.Service.Web.Data.Validators;

public class CoverValidator: AbstractValidator<Cover>
{
    public CoverValidator() 
    {
        RuleFor(x => x.CoverImageHttpLink).NotEmpty();
    }
}
