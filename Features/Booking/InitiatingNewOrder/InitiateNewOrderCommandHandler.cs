using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Data.Models;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Features.Booking.InitiatingNewOrder;

public record InitiateNewOrderCommand(Guid UserId, Guid BookId);

public class InitiateNewOrderCommandHandler
{
    private readonly IDocumentSession session;

    public InitiateNewOrderCommandHandler(IDocumentSession documentSession)
    {
        this.session = documentSession;
    }

    public async Task<Result> HandleAsync(InitiateNewOrderCommand command, CancellationToken c)
    {
        return await Order.Create(session, command.UserId, command.BookId)
            .Tap(order => session.Store(order))
            .Tap(() => session.SaveChangesAsync(c));
    }
}
