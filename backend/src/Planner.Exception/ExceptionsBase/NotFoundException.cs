using System.Net;

namespace Planner.Exception.ExceptionsBase;
public class NotFoundException : PlannerException
{
    public NotFoundException(string message): base(message)
    {
        
    }

    public override IList<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetStatusCode()
    {
       return HttpStatusCode.NotFound;
    }
}
