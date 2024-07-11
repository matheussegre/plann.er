using Planner.Exception;
using Planner.Exception.ExceptionsBase;
using Planner.Infrastructure;
using Planner.Infrastructure.Enums;

namespace Planner.Application.UseCases.Trips.Activities.Complete;

public class CompleteActivityForTripUseCase
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

        activity.Status = ActivityStatus.Done;

        dbContext.Activities.Update(activity);
        dbContext.SaveChanges();
    }
}