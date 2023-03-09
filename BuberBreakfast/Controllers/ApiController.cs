using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuberBreakfast.Controllers;

[ApiController]
[Route("[controller]")]
//base controller for the other controllers
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        //check if all errors are validation errors
        if (errors.All(e => e.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem(modelStateDictionary);
        }

        //if any error is an unexpected error
        if (errors.Any(e => e.Type == ErrorType.Unexpected))
        {
            return Problem();
        }
        var FirstError = errors[0];
        //swicth case on various error types
        var statusCode = FirstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError

        };

        return Problem(statusCode: statusCode, title: FirstError.Description);

    }

}