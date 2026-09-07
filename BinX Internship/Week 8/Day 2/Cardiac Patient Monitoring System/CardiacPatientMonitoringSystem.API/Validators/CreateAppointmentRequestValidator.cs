using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using FluentValidation;

namespace CardiacPatientMonitoringSystem.API.Validators
{
    public class CreateAppointmentRequestValidator : AbstractValidator<CreateAppointmentRequest>
    {
        public CreateAppointmentRequestValidator()
        {
            RuleFor(x => x.AppointmentDate)
                .NotEmpty()
                .GreaterThan(DateTime.Now);

            RuleFor(x => x.Reason)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.Notes));
        }
    }
}