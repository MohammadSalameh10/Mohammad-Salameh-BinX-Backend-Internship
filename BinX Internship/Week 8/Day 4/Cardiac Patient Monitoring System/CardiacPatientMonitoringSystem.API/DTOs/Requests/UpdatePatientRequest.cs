namespace CardiacPatientMonitoringSystem.API.DTOs.Requests
{
    public class UpdatePatientRequest
    {
        public string FullName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string BloodType { get; set; } = string.Empty;
    }
}