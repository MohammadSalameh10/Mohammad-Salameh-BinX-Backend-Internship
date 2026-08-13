namespace CardiacPatientMonitoringSystem.API.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string BloodType { get; set; } = string.Empty;
    }
}