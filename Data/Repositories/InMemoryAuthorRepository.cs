using KbAis.Intern.Library.Service.Web.Data.Interfaces;
using KbAis.Intern.Library.Service.Web.Data.Models;
using System.Collections.Concurrent;

namespace KbAis.Intern.Library.Service.Web.Data.Repositories;

public class InMemoryAuthorRepository : IAuthorRepository
{
    static int concurrencyLevel = Environment.ProcessorCount * 2;
    ConcurrentDictionary<int, Author> AuthorDictionary = new ConcurrentDictionary<int, Author>(concurrencyLevel, 128);
    int authorMaxId = -1;

    public void AddAuthor(Author author)
    {
        authorMaxId++;
        AuthorDictionary[authorMaxId] = author;
    }

    public void DeleteAuthor(int id)
    {
        _ = AuthorDictionary.TryRemove(id, out _);
    }

    public Author GetAuthorById(int id)
    {
        return AuthorDictionary[id];
    }

    public IEnumerable<Author> GetAuthorByName(string name)
    {
        foreach (Author author in AuthorDictionary.Values)
        {
            string authorName = author.FirstName + " " + author.SecondName;
            if (authorName.IndexOf(name) != -1)
            {
                yield return author;
            }
        }
    }
}
