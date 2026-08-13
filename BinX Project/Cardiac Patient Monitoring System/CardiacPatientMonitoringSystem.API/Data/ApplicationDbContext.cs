using CardiacPatientMonitoringSystem.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<VitalSign> VitalSigns { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}