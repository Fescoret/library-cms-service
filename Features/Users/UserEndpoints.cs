using KbAis.Intern.Library.Service.Web.Features.Users.RegisteringUser.V1;
using KbAis.Intern.Library.Service.Web.Features.Users.UpdatingUserInfo.V1;
using KbAis.Intern.Library.Service.Web.Features.Users.DeletingUser.V1;
using KbAis.Intern.Library.Service.Web.Features.Users.GetingUserById.V1;
using KbAis.Intern.Library.Service.Web.Features.Users.GettingAllUsers.V1;
using KbAis.Intern.Library.Service.Web.Features.Users.LoginingUser.V1;

namespace KbAis.Intern.Library.Service.Web.Features.Users;

public static class UserEndpoints
{
    public const string ResourceName = "users";

    public static class V1
    {
        public static void Map(WebApplication app)
        {
            GetUserByIdEndpoint.Map(app);

            GetAllUsersEndpoint.Map(app);

            RegisterUserEndpoint.Map(app);

            LoginUserEndpoint.Map(app);

            DeleteUserEndpoint.Map(app);

            UpdateUserEndpoint.Map(app);
        }
    }
}
