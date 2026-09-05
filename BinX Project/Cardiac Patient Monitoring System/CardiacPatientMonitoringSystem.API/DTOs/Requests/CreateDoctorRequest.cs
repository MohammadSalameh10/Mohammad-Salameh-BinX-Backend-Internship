namespace CardiacPatientMonitoringSystem.API.DTOs.Requests
{
    public class CreateDoctorRequest
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}