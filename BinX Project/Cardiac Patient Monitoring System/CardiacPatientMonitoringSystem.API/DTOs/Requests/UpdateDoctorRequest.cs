namespace CardiacPatientMonitoringSystem.API.DTOs.Requests
{
    public class UpdateDoctorRequest
    {
        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}