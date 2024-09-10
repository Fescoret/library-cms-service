using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Booking.IssuingOrder;

public record IssueOrderCommand(Guid OrderId);

public class IssueOrderCommandHandler
{
    private readonly IDocumentSession session;

    public IssueOrderCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result> HandleAsync(IssueOrderCommand command, CancellationToken c)
    {
        return await session.Load<Order>(command.OrderId).AsMaybe()
            .ToResult("Order with such id not found")
            .Tap(order => session.Update(order.Issue()))
            .Tap(() => session.SaveChangesAsync(c));
    }
}
