using CSharpFunctionalExtensions;
using KbAis.Intern.Library.Service.Web.Utils.FluentValidatorEx;
using Microsoft.AspNetCore.Mvc;

namespace KbAis.Intern.Library.Service.Web.Features.Booking.InitiatingNewOrder.V1;

public class InitiateNewOrderEndpoint
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/orders/create", HandleRequestAsync)
            .WithTags("orders");
    }

    private static async Task<WebResult> HandleRequestAsync(
        [FromServices] InitiateNewOrderCommandHandler commandHandler,
        [FromServices] InitiateNewOrderRequestValidator validator,
        [FromBody] InitiateNewOrderRequest request,
        CancellationToken c
    )
    {
        var result = await validator.ValidateForResult(request)
            .MapError(validationResult =>
                Results.BadRequest(validationResult.ToDictionary()))
            .Bind(x => commandHandler.HandleAsync(new(request.UserId, request.BookId), c)
                .MapError(error => Results.BadRequest(error))
                .Map(() => Results.Created("/profile", null))
            );

        return result.IsSuccess ? result.Value : result.Error;
    }

    public class InitiateNewOrderRequest
    {
        public Guid UserId { get; init; }

        public Guid BookId { get; init; }
    }
}
