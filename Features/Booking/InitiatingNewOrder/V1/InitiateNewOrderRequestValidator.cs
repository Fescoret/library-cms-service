using FluentValidation;

namespace KbAis.Intern.Library.Service.Web.Features.Booking.InitiatingNewOrder.V1;

public class InitiateNewOrderRequestValidator : AbstractValidator<InitiateNewOrderEndpoint.InitiateNewOrderRequest>
{
    public InitiateNewOrderRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.BookId).NotEmpty();
    }
}
