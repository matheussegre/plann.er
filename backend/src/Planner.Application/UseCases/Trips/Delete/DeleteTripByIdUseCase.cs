using Microsoft.EntityFrameworkCore;
using Planner.Exception;
using Planner.Exception.ExceptionsBase;
using Planner.Infrastructure;

namespace Planner.Application.UseCases.Trips.Delete;
public class DeleteTripByIdUseCase
{
    public void Execute(Guid id)
    {
        var dbContext = new PlannerDbContext();

        var trip = dbContext.Trips
            .Include(trip => trip.Activities)
            .FirstOrDefault(trip => trip.Id.Equals(id));

        if (trip is null) throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
    
        dbContext.Trips.Remove(trip);
        dbContext.SaveChanges();
    }
}
