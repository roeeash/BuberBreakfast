using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

public class ErrorsController : ApiController
{
    /**
    replace route to error
    implement error handling logic
    */
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }


}