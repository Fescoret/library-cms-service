using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;

namespace KbAis.Intern.Library.Service.Web.Utils.FluentValidatorEx;

public static class AbstractValidatorEx
{
    public static async Task<Result<T, ValidationResult>> ValidateForResult<T>(
        this AbstractValidator<T> validator, T validatble
    ) {
        var validationResult = await validator.ValidateAsync(validatble);

        return Result.SuccessIf(validationResult.Errors.Any() == false, validatble, validationResult);
    }
}
