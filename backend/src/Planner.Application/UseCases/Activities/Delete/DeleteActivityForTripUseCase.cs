using Planner.Exception;
using Planner.Exception.ExceptionsBase;
using Planner.Infrastructure;

namespace Planner.Application.UseCases.Trips.Activities.Delete;

public class DeleteActivityForTripUseCase
{
    public void Execute(Guid tripId, Guid activityId)
    {
        var dbContext = new PlannerDbContext();
        var activity = dbContext
            .Activities
            .FirstOrDefault(activity => activity.Id.Equals(activityId) 
                                        && activity.TripId.Equals(tripId));
        
        if (activity is null)
            throw new NotFoundException(ResourceErrorMessages.ACTIVITY_NOT_FOUND);

        dbContext.Activities.Remove(activity);
        dbContext.SaveChanges();
    }
}