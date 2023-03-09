using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreakfastsController : ApiController
    {
        private readonly IBreakfastService _breakfastService;

        public BreakfastsController(IBreakfastService breakfastService)
        {
            _breakfastService = breakfastService;
        }

        [HttpPost]
        public IActionResult CreateBreakfast(CreateBreakfastRequest request)
        {
            //try to create a new request
            ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(request);
            //check if we got an error
            if (requestToBreakfastResult.IsError)
            {
                return Problem(requestToBreakfastResult.Errors);
            }

            //otherwise, it's a breakfast
            var breakfast = requestToBreakfastResult.Value;
            //logic for the creation process
            ErrorOr<Created> createBreakFastResult = _breakfastService.CreateBreakfast(breakfast);

            //check if it is an error
            return createBreakFastResult.Match(
              created => CreatedAsGetBreakfast(breakfast),
              errors => Problem(errors)
          );
        }


        [HttpGet("{id:guid}")]
        public IActionResult GetBreakfast(Guid id)
        {
            ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

            return getBreakfastResult.Match(
                breakfast => Ok(MapBreakfastResponse(breakfast)),
                errors => Problem(errors)
            );
            /**if (getBreakfastResult.IsError &&
            getBreakfastResult.FirstError == Errors.Breakfast.NotFound)
            {
                return NotFound();
            }

            //if not an error, get value and send it as a response
            var breakfast = getBreakfastResult.Value;

            BreakfastResponse response = MapBreakfastResponse(breakfast);

            return Ok(response);
            **/
        }


        [HttpPut("{id:guid}")]
        public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
        {
            //taking the data and mappping it to our application
            //using the id we got
            ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(id, request);
            //check if we got an error
            if (requestToBreakfastResult.IsError)
            {
                return Problem(requestToBreakfastResult.Errors);
            }

            //otherwise, it's a breakfast
            var breakfast = requestToBreakfastResult.Value;

            //update breakfast
            var upsertBreakfastResult = _breakfastService.UpsertBreakfast(breakfast);
            //return 204 - No content if breakfast was updated
            //otherwise, return 201 - created
            return upsertBreakfastResult.Match(
                upserted => upserted.IsNewlyCreated ? CreatedAsGetBreakfast(breakfast) : NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteBreakfast(Guid id)
        {
            //delete breakfast
            ErrorOr<Deleted> deleteBreakfastResult = _breakfastService.DeleteBreakfast(id);

            return deleteBreakfastResult.Match(
                deleted => NoContent(),
                errors => Problem(errors)
            );
        }


        private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
        {
            // map breakfast to breakfast response
            return new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                breakfast.LastModifiedDateTime,
                breakfast.Savory,
                breakfast.Sweet
            );
        }


        private IActionResult CreatedAsGetBreakfast(Breakfast breakfast)
        {
            // return the newly created breakfast with its unique ID
            return CreatedAtAction(nameof(GetBreakfast), new { id = breakfast.Id },
            MapBreakfastResponse(breakfast));
        }
    }
}