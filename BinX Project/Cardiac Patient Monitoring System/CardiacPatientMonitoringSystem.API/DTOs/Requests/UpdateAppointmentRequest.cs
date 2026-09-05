namespace CardiacPatientMonitoringSystem.API.DTOs.Requests
{
    public class UpdateAppointmentRequest
    {
        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Reason { get; set; } = string.Empty;

        public string? Notes { get; set; }
    }
}