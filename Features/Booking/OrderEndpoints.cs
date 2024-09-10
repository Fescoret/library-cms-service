using KbAis.Intern.Library.Service.Web.Features.Booking.GettingAllOrders.V1;
using KbAis.Intern.Library.Service.Web.Features.Booking.InitiatingNewOrder.V1;
using KbAis.Intern.Library.Service.Web.Features.Booking.IssuingOrder.V1;
using KbAis.Intern.Library.Service.Web.Features.Booking.ReturningOrder.V1;

namespace KbAis.Intern.Library.Service.Web.Features.Booking;

public static class OrderEndpoints
{
    public const string ResourceName = "orders";

    public static class V1
    {
        public static void Map(WebApplication app)
        {
            GetAllOrdersEndpoint.Map(app);

            InitiateNewOrderEndpoint.Map(app);

            IssueOrderEndpoint.Map(app);

            ReturnOrderEndpoint.Map(app);
        }
    }
}
