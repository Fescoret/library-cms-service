using CSharpFunctionalExtensions;
using Marten;
using Marten.Schema;

namespace KbAis.Intern.Library.Service.Web.Data.Models;


[SoftDeleted]
public class Book
{
    public Guid Id { get; protected init; }

    public string Title { get; protected set; } = null!;

    public ICollection<Author> Authors { get; protected set; } = null!;

    public Cover Cover { get; protected set; } = null!;

    public Book()
    {

    }

    public static Result<Book> Create(string title, ICollection<Author> authors, Cover cover)
    {
        var isPropertiesCorrect = Result.Combine(
            Result.SuccessIf(string.IsNullOrEmpty(title) == false, "Title is requeried"),
            Result.SuccessIf((authors is not null) && authors.Any(), "At least one author is requeried"),
            Result.SuccessIf((cover is null) == false, "Cover shouldn't be null")
        );
        return isPropertiesCorrect.Map(() =>
            new Book() { Title = title, Authors = authors, Cover = cover }
        );
    }

    public Result<Book> UpdateBook(string title, ICollection<Author> authors, Cover cover)
    {
        var isPropertiesCorrect = Result.Combine(
            Result.SuccessIf(string.IsNullOrEmpty(title) == false, "Title is requeried"),
            Result.SuccessIf((authors is not null) && authors.Any(), "At least one author is requeried"),
            Result.SuccessIf((cover is null) == false, "Cover shouldn't be null")
        );
        return isPropertiesCorrect.Map(() =>
        {
            Title = title;
            Authors = authors;
            Cover = cover;
            return this;
        });
    }

    public Result<Book> DeleteBook(IDocumentSession session)
    {
        var queryOrder = session.Query<Order>().Where(x => (x.BookId == Id) && !x.IsReturned).FirstOrDefault();
        Result isPropertiesCorrect;
        if (queryOrder != null)
        {
            isPropertiesCorrect = Result.SuccessIf(queryOrder.IsExpired, 
                "There is an active and not expired order with this book");
        }
        else isPropertiesCorrect = Result.Success();
        return isPropertiesCorrect.Map(() =>
        {
            return this;
        });
    }
}
