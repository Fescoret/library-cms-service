using KbAis.Intern.Library.Service.Web.Data.Interfaces;
using KbAis.Intern.Library.Service.Web.Data.Models;
using System.Collections.Concurrent;

namespace KbAis.Intern.Library.Service.Web.Data.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    static int concurrencyLevel = Environment.ProcessorCount * 2;
    ConcurrentDictionary<int, Book> BookDictionary = new ConcurrentDictionary<int, Book>(concurrencyLevel, 128);
    int bookMaxId = -1;

    public void AddBook(Book book)
    {
        bookMaxId++;
        BookDictionary[bookMaxId] = book;
    }

    public void DeleteBook(int id)
    {
        _ = BookDictionary.TryRemove(id, out _);
    }

    public IEnumerable<Book> GetAllBooks()
    {
        foreach (Book book in BookDictionary.Values)
        {
            yield return book;
        }
    }

    public Book GetBookById(int id)
    {
        return BookDictionary[id];
    }

    public IEnumerable<Book> GetBooksByName(string name)
    {
        foreach (Book book in BookDictionary.Values)
        {
            string bookName = book.Title;
            if (bookName.IndexOf(name) != -1)
            {
                yield return book;
            }
        }
    }
}
