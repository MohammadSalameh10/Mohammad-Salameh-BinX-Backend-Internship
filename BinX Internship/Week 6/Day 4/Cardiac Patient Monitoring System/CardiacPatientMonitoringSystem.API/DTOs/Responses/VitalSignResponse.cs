namespace CardiacPatientMonitoringSystem.API.DTOs.Responses
{
    public class VitalSignResponse
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int HeartRate { get; set; }
        public int SystolicBloodPressure { get; set; }
        public int DiastolicBloodPressure { get; set; }
        public int OxygenSaturation { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}