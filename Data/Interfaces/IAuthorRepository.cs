using KbAis.Intern.Library.Service.Web.Data.Models;

namespace KbAis.Intern.Library.Service.Web.Data.Interfaces;

public interface IAuthorRepository
{
    Author GetAuthorById(int id);

    IEnumerable<Author> GetAuthorByName(string name);

    void AddAuthor(Author author);

    void DeleteAuthor(int id);
}
