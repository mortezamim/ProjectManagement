using FluentValidation.Results;

namespace Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IReadOnlyCollection<ValidationError> errors)
        : base("Validation failed")
    {
        Errors = errors;
    }
    public ValidationException(IReadOnlyCollection<ValidationFailure> errors)
        : base("Validation failed")
    {
        Errors = errors
            .Select(validationFailure => new ValidationError(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage))
            .ToList();
    }

    public IReadOnlyCollection<ValidationError> Errors { get; }
}

public record ValidationError(string PropertyName, string ErrorMessage);