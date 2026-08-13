using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using FluentValidation;

namespace CardiacPatientMonitoringSystem.API.Validators
{
    public class CreatePatientRequestValidator : AbstractValidator<CreatePatientRequest>
    {
        public CreatePatientRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .LessThan(DateTime.Today);

            RuleFor(x => x.Gender)
                .NotEmpty();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.BloodType)
                .NotEmpty()
                .MaximumLength(5);
        }
    }
}