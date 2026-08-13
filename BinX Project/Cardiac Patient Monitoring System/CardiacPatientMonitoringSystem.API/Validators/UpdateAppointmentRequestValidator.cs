using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using FluentValidation;

namespace CardiacPatientMonitoringSystem.API.Validators
{
    public class UpdateAppointmentRequestValidator : AbstractValidator<UpdateAppointmentRequest>
    {
        public UpdateAppointmentRequestValidator()
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