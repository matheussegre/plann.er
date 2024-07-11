using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Planner.Communication.Requests;
using Planner.Communication.Responses;
using Planner.Exception;
using Planner.Exception.ExceptionsBase;
using Planner.Infrastructure;
using Planner.Infrastructure.Entities;

namespace Planner.Application.UseCases.Trips.Activities.Register;

public class RegisterActivityForTripUseCase
{
    public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
    {
        var dbContext = new PlannerDbContext();
        var trip = dbContext
            .Trips
            .FirstOrDefault(trip => trip.Id.Equals(tripId));

        Validate(trip, request);

        var entity = new Activity
        {
            Name = request.Name,
            Date = request.Date,
            TripId = tripId,
        };

        dbContext.Activities.Add(entity);
        dbContext.SaveChanges();

        return new ResponseActivityJson
        {
            Id = entity.Id,
            Name = entity.Name,
            Date = entity.Date,
            Status = (Communication.Enums.ActivityStatus)entity.Status,
        };
    }

    private void Validate(Trip? trip, RequestRegisterActivityJson request)
    {
        if (trip is null) throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);

        var validator = new RegisterActivityValidator();
        var result = validator.Validate(request);

        if ((request.Date >= trip.StartDate && request.Date <= trip.EndDate) == false)
            result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessages.DATE_NOT_WITHIN_TRAVEL_PERIOD));
        

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}