using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Booking.IssuingOrder.V1
{
    public class IssueOrderEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapPut($"/orders/{{order-id}}/issue", HandleRequestAsync)
                .WithTags("orders");
        }

        private static async Task<WebResult> HandleRequestAsync(
            [FromRoute(Name = "order-id")] Guid orderId,
            [FromServices] IssueOrderCommandHandler commandHandler,
            CancellationToken c
        )
        {
            var result = await commandHandler.HandleAsync(new(orderId), c)
                .MapError(error => Results.BadRequest(error))
                .Map(() => Results.Ok("Order successfully issued"));

            return result.IsSuccess ? result.Value : result.Error;
        }
    }
}
