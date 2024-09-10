using CSharpFunctionalExtensions;
using Marten;

namespace KbAis.Intern.Library.Service.Web.Data.Models;

public class Order
{
    public Guid Id { get; protected init; }

    public Guid UserId { get; protected set; }

    public Guid BookId { get; protected set; }

    public long InitialDate { get; protected set; }

    public bool IsIssued { get; protected set; }

    public bool IsReturned { get; protected set; }

    public bool IsExpired { get; protected set; }

    public Order()
    {

    }

    public static Result<Order> Create(IDocumentSession session, Guid userId, Guid bookId)
    {
        var isPropertiesCorrect = Result.Combine(
            Result.SuccessIf(userId.ToString() != "00000000-0000-0000-0000-000000000000", "UserId is requeried"),
            Result.SuccessIf(bookId.ToString() != "00000000-0000-0000-0000-000000000000", "BookId is requeried")
        );
        return isPropertiesCorrect.Map(() =>
            new Order()
            {
                UserId = userId,
                BookId = bookId,
                InitialDate = DateTime.Now.Ticks,
                IsIssued = false,
                IsReturned = false,
                IsExpired = false
            }
        ).Bind(order => session.Load<User>(userId).AsMaybe()
            .ToResult("User not found (maybe it is deleted)")
            .Bind(user => user.AddActiveOrder(order.Id))
            .Tap(user => session.Update(user))
            .Map(user => { return order; }));
    }

    public Order Issue()
    {
        IsIssued = true;
        return this;
    }

    public Order Return()
    {
        IsReturned = true;
        return this;
    }

    public Order Expire()
    {
        IsExpired = true;
        return this;
    }
}
