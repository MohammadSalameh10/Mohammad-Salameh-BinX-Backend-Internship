using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using FluentValidation;

namespace CardiacPatientMonitoringSystem.API.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}