using FluentValidation;
using Planner.Communication.Requests;

namespace Planner.Application.UseCases.Trips.Activities.Register;

public class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
{
    public RegisterActivityValidator()
    { 
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Name must not be");
    }
}