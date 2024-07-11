using Microsoft.AspNetCore.Mvc;
using Planner.Application.UseCases.Trips.Activities.Complete;
using Planner.Application.UseCases.Trips.Activities.Delete;
using Planner.Application.UseCases.Trips.Activities.Register;
using Planner.Application.UseCases.Trips.Delete;
using Planner.Application.UseCases.Trips.GetAll;
using Planner.Application.UseCases.Trips.GetById;
using Planner.Application.UseCases.Trips.Register;
using Planner.Communication.Requests;
using Planner.Communication.Responses;
using Planner.Exception.ExceptionsBase;

namespace Planner.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestRegisterTripJson request)
    {
        var useCase = new RegisterTripUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var useCase = new GetAllTripsUseCase();

        var result = useCase.Execute();

        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var useCase = new GetTripByIdUseCase();

        var response = useCase.Execute(id);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        var useCase = new DeleteTripByIdUseCase();

        useCase.Execute(id);

        return NoContent();
    }

    [HttpPost]
    [Route("{tripId}/activity")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult RegisterActivity([FromRoute] Guid tripId,
        [FromBody] RequestRegisterActivityJson request)
    {
        var useCase = new RegisterActivityForTripUseCase();

        var response = useCase.Execute(tripId, request);

        return Created(string.Empty,response);
    }

    [HttpPut]
    [Route("{tripId}/activity/{activityId}/complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult CompleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
    {
        var useCase = new CompleteActivityForTripUseCase();

        useCase.Execute(tripId, activityId);
        
        return NoContent();
    }

    [HttpDelete]
    [Route("{tripId}/activity/{activityId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult DeleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
    {
        var useCase = new DeleteActivityForTripUseCase();

        useCase.Execute(tripId, activityId);

        return NoContent();
    }
}
