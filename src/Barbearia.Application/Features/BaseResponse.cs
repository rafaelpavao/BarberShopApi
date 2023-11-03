using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features;

public enum Error { ValidationProblem, NotFoundProblem, BadRequestProblem, InternalServerErrorProblem}
public abstract class BaseResponse
{
    public bool IsSuccess
    {
        get { return Errors.Count == 0; }
    }
    
    public Dictionary<string, string[]> Errors { get; set; } = new();

    public Error? ErrorType { get; set; } = null;

    public void FillErrors(ValidationResult validationResult)
    {
        foreach (var error in validationResult.ToDictionary())
        {
            Errors.Add(error.Key, error.Value);
        }
    }

    private void FillErrorType(Error errorType)
    {
        ErrorType = errorType;
    }

    public void AddErrors(ValidationResult validationResult, Error errorType)
    {
        FillErrors(validationResult);
        FillErrorType(errorType);
    }
}