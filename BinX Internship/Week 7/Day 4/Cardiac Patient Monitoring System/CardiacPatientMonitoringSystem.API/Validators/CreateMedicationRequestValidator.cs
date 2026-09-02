using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using FluentValidation;

namespace CardiacPatientMonitoringSystem.API.Validators
{
    public class CreateMedicationRequestValidator : AbstractValidator<CreateMedicationRequest>
    {
        public CreateMedicationRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Dosage)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Frequency)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.StartDate)
                .NotEmpty();

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.EndDate.HasValue);
        }
    }
}