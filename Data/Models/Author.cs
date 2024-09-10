using CSharpFunctionalExtensions;
using Marten.Schema;

namespace KbAis.Intern.Library.Service.Web.Data.Models;

//[SoftDeleted]
public class Author
{
    public Guid Id { get; protected init; }

    public string FirstName { get; protected set; } = null!;

    public string? SecondName { get; protected set; }

    public Author()
    {

    }

    public static Result<Author> Create(string firstName, string? secondName)
    {
        var isPropertiesCorrect = Result.Combine(
            Result.SuccessIf(string.IsNullOrEmpty(firstName) == false, "FirstName is requeried"),
            Result.SuccessIf((secondName is null) == false, "SecondName shouldn't be null")
        );
        return isPropertiesCorrect.Map(() =>
            new Author() { FirstName = firstName, SecondName = secondName }
        );
    }

    public Result<Author> UpdateAuthor(string firstName, string? secondName)
    {
        var isPropertiesCorrect = Result.Combine(
            Result.SuccessIf(string.IsNullOrEmpty(firstName) == false, "FirstName is requeried"),
            Result.SuccessIf((secondName is null) == false, "SecondName shouldn't be null")
        );
        return isPropertiesCorrect.Map(() =>
        {
            FirstName = firstName;
            SecondName = secondName;
            return this;
        });
        
    }
}
