using FluentValidation.Results;

namespace Domain.Exception;

public class ResturantValidationException : ApplicationException
{
    public List<string> ValdationErrors { get; set; }

    public ResturantValidationException(string error)
    {
        ValdationErrors ??= new List<string>();
        ValdationErrors.Add($"validation error for customer:- {error}");
    }
    public ResturantValidationException(ValidationResult validationResult)
    {
        ValdationErrors = new List<string>();

        foreach (var validationError in validationResult.Errors)
        {
            ValdationErrors.Add(validationError.ErrorMessage);
        }
    }

    public ResturantValidationException(List<ValidationFailure> validationFailures)
    {
        ValdationErrors = new List<string>();

        foreach (var validationError in validationFailures)
        {
            ValdationErrors.Add(validationError.ErrorMessage);
        }
    }
}
