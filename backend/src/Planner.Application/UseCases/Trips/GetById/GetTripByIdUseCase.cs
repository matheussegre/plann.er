using Microsoft.EntityFrameworkCore;
using Planner.Communication.Responses;
using Planner.Exception;
using Planner.Exception.ExceptionsBase;
using Planner.Infrastructure;

namespace Planner.Application.UseCases.Trips.GetById;
public class GetTripByIdUseCase
{
    public ResponseTripJson Execute(Guid id)
    {
        var dbContext = new PlannerDbContext();

        var trip = dbContext.Trips
            .Include(trip => trip.Activities)
            .FirstOrDefault(trip => trip.Id.Equals(id));

        if (trip is null) throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);

        return new ResponseTripJson
        {
            Id = trip.Id,
            Name = trip.Name,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            Activities = trip.Activities.Select(activity => new ResponseActivityJson
            {
                Id = activity.Id,
                Name = activity.Name,
                Date = activity.Date,
                Status = (Communication.Enums.ActivityStatus)activity.Status,
            }).ToList(),
        };
    }
}
