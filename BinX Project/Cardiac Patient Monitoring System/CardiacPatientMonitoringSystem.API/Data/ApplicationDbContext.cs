using CardiacPatientMonitoringSystem.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<VitalSign> VitalSigns { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.VitalSigns)
                .WithOne(v => v.Patient)
                .HasForeignKey(v => v.PatientId);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Medications)
                .WithOne(m => m.Patient)
                .HasForeignKey(m => m.PatientId);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId);
        }
    }
}