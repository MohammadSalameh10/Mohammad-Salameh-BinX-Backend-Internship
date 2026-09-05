namespace CardiacPatientMonitoringSystem.API.DTOs.Responses
{
    public class DoctorResponse
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}