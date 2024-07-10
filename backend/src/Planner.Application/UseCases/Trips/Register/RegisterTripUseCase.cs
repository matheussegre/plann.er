using Planner.Communication.Requests;
using Planner.Communication.Responses;
using Planner.Exception;
using Planner.Exception.ExceptionsBase;
using Planner.Infrastructure;
using Planner.Infrastructure.Entities;

namespace Planner.Application.UseCases.Trips.Register;
public class RegisterTripUseCase
{
    public ResponseShortTripJson Execute(RequestRegisterTripJson request)
    {
        Validate(request);

        var dbContext = new PlannerDbContext();

        var entity = new Trip
        {
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };

        dbContext.Trips.Add(entity);

        dbContext.SaveChanges();

        return new ResponseShortTripJson
        {
            EndDate = entity.EndDate,
            StartDate = entity.StartDate,
            Name = entity.Name,
            Id = entity.Id,
        };
    }

    private void Validate(RequestRegisterTripJson request)
    {
        var validator = new RegisterTripValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
            
    }
}
