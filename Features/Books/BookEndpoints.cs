using KbAis.Intern.Library.Service.Web.Features.Books.GettingBookById.V1;
using KbAis.Intern.Library.Service.Web.Features.Books.GettingAllBooks.V1;
using KbAis.Intern.Library.Service.Web.Features.Books.CreatingBook.V1;
using KbAis.Intern.Library.Service.Web.Features.Books.DeletingBook.V1;
using KbAis.Intern.Library.Service.Web.Features.Books.UpdatingBookInfo.V1;

namespace KbAis.Intern.Library.Service.Web.Features.Books;

public static class BookEndpoints
{
    public const string ResourceName = "books";

    public static class V1
    {
        public static void Map(WebApplication app)
        {
            GetBookByIdEndpoint.Map(app);

            GetAllBooksEndpoint.Map(app);

            CreateBookEndpoint.Map(app);

            DeleteBookEndpoint.Map(app);

            UpdateBookEndpoint.Map(app);
        }
    }
}
