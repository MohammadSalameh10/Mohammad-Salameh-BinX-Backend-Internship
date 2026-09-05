using Microsoft.AspNetCore.Identity;

namespace CardiacPatientMonitoringSystem.API.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public IdentityUser User { get; set; } = null!;

        public ICollection<Appointment> Appointments { get; set; }
            = new List<Appointment>();
    }
}
