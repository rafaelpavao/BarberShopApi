using System.Diagnostics;
using Barbearia.Application.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace Barbearia.Api.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    // protected const int maxPageSize = 6;

    public ActionResult HandleRequestError(BaseResponse response)
    {
        ConfigureModelState(response.Errors);

        switch(response.ErrorType)
        {
            case Error.ValidationProblem:return UnprocessableEntity(ModelState);
            case Error.NotFoundProblem:return NotFound(ModelState);
            case Error.BadRequestProblem:return BadRequest(ModelState);
            case Error.InternalServerErrorProblem:return InternalServerErrorProblem(ModelState);
            default:
                return StatusCode(StatusCodes.Status418ImATeapot);
        }
    }
    public override ActionResult ValidationProblem(
    [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
    {
        // base.ValidationProblem();
        var options = HttpContext.RequestServices
            .GetRequiredService<IOptions<ApiBehaviorOptions>>();

        return (ActionResult)options.Value
            .InvalidModelStateResponseFactory(ControllerContext);
    }

    public override NotFoundObjectResult NotFound([ActionResultObjectValue] object? value)
    {
        Console.WriteLine(ModelState.ErrorCount);
        Console.WriteLine(ModelState.IsValid);
        Console.WriteLine(ModelState == null);
        Console.WriteLine(Activity.Current?.Id);

        // Cria a fábrica de um objeto de detalhes de problema de validação
        var problemDetailsFactory = HttpContext.RequestServices
            .GetRequiredService<ProblemDetailsFactory>();

        // Cria um objeto de detalhes de problema de validação
        var validationProblemDetails = problemDetailsFactory
            .CreateValidationProblemDetails(
                HttpContext,
                ModelState!);

        // Adiciona informações adicionais não adicionadas por padrão
        validationProblemDetails.Detail =
            "See the errors field for details.";
        validationProblemDetails.Instance =
            HttpContext.Request.Path;

        // Relata respostas do estado de modelo inválido como problemas de validação
        validationProblemDetails.Type =
            "https://courseunivali.com/notfoundproblem";
        validationProblemDetails.Status =
            StatusCodes.Status404NotFound;
        validationProblemDetails.Title =
            "One or more records were not found";
        // validationProblemDetails.Extensions["traceId"] = HttpContext.TraceIdentifier;
        return new NotFoundObjectResult(
            validationProblemDetails)
        {
            ContentTypes = { "application/problem+json" }
        };

    }

    public override BadRequestObjectResult BadRequest(ModelStateDictionary modelStateDictionary)
    {
        Console.WriteLine(ModelState.ErrorCount);
        Console.WriteLine(ModelState.IsValid);
        Console.WriteLine(ModelState == null);
        Console.WriteLine(Activity.Current?.Id);

        // Cria a fábrica de um objeto de detalhes de problema de validação
        var problemDetailsFactory = HttpContext.RequestServices
            .GetRequiredService<ProblemDetailsFactory>();

        // Cria um objeto de detalhes de problema de validação
        var validationProblemDetails = problemDetailsFactory
            .CreateValidationProblemDetails(
                HttpContext,
                ModelState!);

        // Adiciona informações adicionais não adicionadas por padrão
        validationProblemDetails.Detail =
            "See the errors field for details.";
        validationProblemDetails.Instance =
            HttpContext.Request.Path;

        // Relata respostas do estado de modelo inválido como problemas de validação
        validationProblemDetails.Type =
            "https://courseunivali.com/badrequestproblem";
        validationProblemDetails.Status =
            StatusCodes.Status400BadRequest;
        validationProblemDetails.Title =
            "One or more issues were found within the request body";
        // validationProblemDetails.Extensions["traceId"] = HttpContext.TraceIdentifier;
        return new BadRequestObjectResult(
            validationProblemDetails)
        {
            ContentTypes = { "application/problem+json" }
        };

    }

    public override UnprocessableEntityObjectResult UnprocessableEntity([
        ActionResultObjectValue] ModelStateDictionary modelState)
    {
        // Cria a fábrica de um objeto de detalhes de problema de validação
        var problemDetailsFactory = HttpContext.RequestServices
            .GetRequiredService<ProblemDetailsFactory>();

        // Cria um objeto de detalhes de problema de validação
        var validationProblemDetails = problemDetailsFactory
            .CreateValidationProblemDetails(
                HttpContext,
                ModelState);

        // Adiciona informações adicionais não adicionadas por padrão
        validationProblemDetails.Detail =
            "See the errors field for details.";
        validationProblemDetails.Instance =
            HttpContext.Request.Path;

        // Relata respostas do estado de modelo inválido como problemas de validação
        validationProblemDetails.Type =
            "https://courseunivali.com/modelvalidationproblem";
        validationProblemDetails.Status =
            StatusCodes.Status422UnprocessableEntity;
        validationProblemDetails.Title =
            "One or more validation errors occurred.";

        return new UnprocessableEntityObjectResult(
            validationProblemDetails)
        {
            ContentTypes = { "application/problem+json" }
        };
    }

    public ObjectResult InternalServerErrorProblem([
        ActionResultObjectValue] ModelStateDictionary modelState)
    {
        // Cria a fábrica de um objeto de detalhes de problema de validação
        var problemDetailsFactory = HttpContext.RequestServices
            .GetRequiredService<ProblemDetailsFactory>();

        // Cria um objeto de detalhes de problema de validação
        var validationProblemDetails = problemDetailsFactory
            .CreateValidationProblemDetails(
                HttpContext,
                ModelState);

        // Adiciona informações adicionais não adicionadas por padrão
        validationProblemDetails.Detail =
            "See the errors field for details.";
        validationProblemDetails.Instance =
            HttpContext.Request.Path;

        // Relata respostas do estado de modelo inválido como problemas de validação
        validationProblemDetails.Type =
            "https://courseunivali.com/modelvalidationproblem";
        validationProblemDetails.Status =
            StatusCodes.Status500InternalServerError;
        validationProblemDetails.Title =
            "One or more validation errors occurred.";

        return new ObjectResult(
            validationProblemDetails)
        {
            ContentTypes = { "application/problem+json" }
        };
    }

    public void ConfigureModelState(Dictionary<string, string[]> errors)
    {
        foreach (var error in errors)
        {
            string key = error.Key;
            string[] values = error.Value;

            foreach (var value in values)
            {
                ModelState.AddModelError(key, value);
            }
        }
    }

    // EXCLUIR DEPOIS
    [NonAction]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    protected ActionResult RequestValidationProblem(
        BaseResponse requestResponse,
        [ActionResultObjectValue] ModelStateDictionary modelStateDictionary
    )
    {
        foreach(var error in requestResponse.Errors)
        {
            string key = error.Key;
            string[] values = error.Value;
            
            foreach (var value in values)
            {
                modelStateDictionary.AddModelError(key, value);
            }
        }

        return ValidationProblem(modelStateDictionary);
    }
}