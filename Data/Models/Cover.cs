using CSharpFunctionalExtensions;
using Marten;
using Marten.Schema;

namespace KbAis.Intern.Library.Service.Web.Data.Models;

//[SoftDeleted]
public class Cover
{
    public Guid Id { get; protected init; }

    public string CoverImageHttpLink { get; protected set; } = null!;

    public Cover()
    {

    }

    public static Result<Cover> Create(string coverImageHttpLink)
    {
        var isProperyCorrect = Result.SuccessIf(string.IsNullOrEmpty(coverImageHttpLink) == false, "Link or image is requeried");
        return isProperyCorrect.Map(() =>
            new Cover() { CoverImageHttpLink = coverImageHttpLink }
        );
    }

    public Result<Cover> UpdateCover(IDocumentSession session, string coverImageHttpLink)
    {
        var isProperyCorrect = Result.SuccessIf(string.IsNullOrEmpty(coverImageHttpLink) == false, "Link or image is requeried");
        return isProperyCorrect.Map(() =>
            {
                CoverImageHttpLink = coverImageHttpLink;
                return this;
            }
        ).Tap(cover =>
        {
            var books = session.Query<Book>().Where(book => (book.Cover.Id == cover.Id)).ToList();
            if (books.Any())
            {
                books.ForEach(book =>
                {
                    book.UpdateBook(book.Title, book.Authors, cover);
                    session.Update(book);
                });
            }
        });        
    }
}
