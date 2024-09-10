using KbAis.Intern.Library.Service.Web.Data.Models;

namespace KbAis.Intern.Library.Service.Web.Data.Interfaces
{
    public interface IBookRepository
    {
        Book GetBookById(int id);

        IEnumerable<Book> GetAllBooks();

        IEnumerable<Book> GetBooksByName(string name);

        void AddBook(Book book);

        void DeleteBook(int id);
    }
}
