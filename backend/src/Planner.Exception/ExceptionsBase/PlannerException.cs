using System.Net;

namespace Planner.Exception.ExceptionsBase;
public abstract class PlannerException: SystemException
{
    public PlannerException(string message): base(message)
    {
        
    }

    public abstract HttpStatusCode GetStatusCode();
    public abstract IList<string> GetErrorMessages();
}
