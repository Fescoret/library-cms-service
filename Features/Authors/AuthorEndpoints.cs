using KbAis.Intern.Library.Service.Web.Features.Authors.GettingAuthorById.V1;
using KbAis.Intern.Library.Service.Web.Features.Authors.GettingAllAuthors.V1;
using KbAis.Intern.Library.Service.Web.Features.Authors.CreateAuthor.V1;
using KbAis.Intern.Library.Service.Web.Features.Authors.DeletingAuthor.V1;
using KbAis.Intern.Library.Service.Web.Features.Authors.UpdateAuthor.V1;

namespace KbAis.Intern.Library.Service.Web.Features.Authors;

public static class AuthorEndpoints
{
    public const string ResourceName = "authors";

    public static class V1
    {
        public static void Map(WebApplication app)
        {
            GetAuthorByIdEndpoint.Map(app);

            GetAllAuthorsEndpoint.Map(app);

            CreateAuthorEndpoint.Map(app);

            DeleteAuthorEndpoint.Map(app);

            UpdateAuthorEndpoint.Map(app);
        }
    }
}