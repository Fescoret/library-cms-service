using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Booking.ReturningOrder;

public record ReturnOrderCommand(Guid OrderId);

public class ReturnOrderCommandHandler
{
    private readonly IDocumentSession session;

    public ReturnOrderCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result> HandleAsync(ReturnOrderCommand command, CancellationToken c)
    {
        return await session.Load<Order>(command.OrderId).AsMaybe()
            .ToResult("Order with such id not found")
            .Bind(order => session.Load<User>(order.UserId).AsMaybe()
                .ToResult("User probably has been deleted")
                .Bind(user => user.RemoveOrder(order.Id))
                .Tap(user => session.Update(user))
                .Tap(() => session.Update(order.Return().Expire())))
            .TapIf(user => !user.IsConscious && !session.LoadMany<Order>(user.Orders)
                    .Where(y => y.IsExpired)
                    .ToList()
                    .Any(),
                user => session.Update(user.ChangeConscious()))
            .Tap(() => session.SaveChangesAsync(c));
    }
}
