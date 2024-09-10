using Baseline;
using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Booking.GettingAllOrders;

public class GetAllOrdersCommandHandler
{
    private readonly IDocumentSession session;

    public GetAllOrdersCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public static void MarkAsExpired(IDocumentSession session, IEnumerable<Order> orders)
    {
        orders.Where(x => x.InitialDate + 15552000 > DateTime.Now.Ticks) // 180 days
            .Each(x =>
            {
                session.Update(x.Expire());
                session.Load<User>(x.UserId).AsMaybe()
                .Execute(user =>
                {
                    if (user.IsConscious) session.Update(user.ChangeConscious());
                });
            });
    }

    public static IEnumerable<Order> ReturnNotIssued(IDocumentSession session, IEnumerable<Order> orders)
    {
        return orders.Where(x => x.InitialDate + 604800 > DateTime.Now.Ticks) // 7 days
            .Each(order =>
            {
                session.Update(order.Return());
                session.Load<User>(order.UserId).AsMaybe()
                .Execute(user => user.RemoveOrder(order.Id)
                    .Tap(user => session.Update(user)));
            });
    }

    public async Task<Result<IEnumerable<Order>>> HandleAsync(CancellationToken c)
    {
        return await session.Query<Order>()
                .Where(x => !x.IsReturned) // only active orders
                .ToList()
            .AsMaybe()
            .ToResult("Server Error: Orders table does'nt exist")
            .Bind(orders => orders.AnyTenant()
                ? Result.Failure<List<Order>>("There is no active orders")
                : Result.Success(orders))
            .Map(orders => ReturnNotIssued(session, orders))
            .Tap(orders => MarkAsExpired(session, orders))
            .Tap(() => session.SaveChangesAsync(c));
    }
}
