namespace CardiacPatientMonitoringSystem.API.DTOs.Requests
{
    public class CreateAppointmentRequest
    {
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}