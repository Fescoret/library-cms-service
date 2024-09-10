using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Booking.ReturningOrder.V1;

public class ReturnOrderEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapPut($"/orders/{{order-id}}/return", HandleRequestAsync)
            .WithTags("orders");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromRoute(Name = "order-id")] Guid orderId,
        [FromServices] ReturnOrderCommandHandler commandHandler,
        CancellationToken c
    )
    {
        var result = await commandHandler.HandleAsync(new(orderId), c)
            .MapError(error => Results.BadRequest(error))
            .Map(() => Results.Ok("Order successfully returned"));

        return result.IsSuccess ? result.Value : result.Error;
    }
}
