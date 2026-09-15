namespace CardiacPatientMonitoringSystem.API.DTOs.Requests
{
    public class CreateAppointmentRequest
    {
        /// <summary>
        /// The requested appointment date and time.
        /// </summary>
        /// <example>2026-09-20T10:30:00</example>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// The reason for the appointment.
        /// </summary>
        /// <example>Cardiology follow-up</example>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Optional notes about the appointment.
        /// </summary>
        /// <example>Patient reports occasional chest discomfort.</example>
        public string? Notes { get; set; }
    }
}