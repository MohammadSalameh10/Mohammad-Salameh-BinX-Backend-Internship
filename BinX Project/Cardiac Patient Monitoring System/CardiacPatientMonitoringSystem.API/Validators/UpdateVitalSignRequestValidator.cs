using CardiacPatientMonitoringSystem.API.DTOs.Requests;
using FluentValidation;

namespace CardiacPatientMonitoringSystem.API.Validators
{
    public class UpdateVitalSignRequestValidator : AbstractValidator<UpdateVitalSignRequest>
    {
        public UpdateVitalSignRequestValidator()
        {
            RuleFor(x => x.HeartRate)
                .GreaterThan(0);

            RuleFor(x => x.SystolicBloodPressure)
                .GreaterThan(0);

            RuleFor(x => x.DiastolicBloodPressure)
                .GreaterThan(0);

            RuleFor(x => x.OxygenSaturation)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.RecordedAt)
                .NotEmpty();
        }
    }
}