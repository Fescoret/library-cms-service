using FluentValidation;
using KbAis.Intern.Library.Service.Web.Data.Models;

namespace KbAis.Intern.Library.Service.Web.Data.Validators;

public class OrderValidator: AbstractValidator<Order>
{
    public OrderValidator() 
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.BookId).NotEmpty();
        RuleFor(x => x.InitialDate).NotNull().LessThan(DateTime.Now.Ticks);
    }
}
