using CardiacPatientMonitoringSystem.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.API.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services
                .GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = services
                .GetRequiredService<UserManager<IdentityUser>>();

            var context = services
                .GetRequiredService<ApplicationDbContext>();

            string[] roles = { "Admin", "Patient", "Doctor" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var roleResult = await roleManager.CreateAsync(
                        new IdentityRole(role));

                    if (!roleResult.Succeeded)
                        return;
                }
            }

            var adminEmail = "admin@cardiac.com";
            var adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                var adminResult = await userManager.CreateAsync(
                    adminUser,
                    adminPassword);

                if (!adminResult.Succeeded)
                    return;
            }

            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                var adminRoleResult = await userManager.AddToRoleAsync(
                    adminUser,
                    "Admin");

                if (!adminRoleResult.Succeeded)
                    return;
            }

            var patientEmail = "patient@cardiac.com";
            var patientPassword = "Patient@123";

            var identityUser = await userManager.FindByEmailAsync(patientEmail);

            if (identityUser == null)
            {
                identityUser = new IdentityUser
                {
                    UserName = patientEmail,
                    Email = patientEmail
                };

                var result = await userManager.CreateAsync(
                    identityUser,
                    patientPassword);

                if (!result.Succeeded)
                    return;
            }

            if (!await userManager.IsInRoleAsync(identityUser, "Patient"))
            {
                var patientRoleResult = await userManager.AddToRoleAsync(
                    identityUser,
                    "Patient");

                if (!patientRoleResult.Succeeded)
                    return;
            }

            var patient = await context.Patients
                .FirstOrDefaultAsync(p => p.UserId == identityUser.Id);

            if (patient == null)
            {
                patient = new Patient
                {
                    UserId = identityUser.Id,
                    FullName = "Test Patient",
                    DateOfBirth = new DateTime(1995, 5, 15),
                    Gender = "Male",
                    PhoneNumber = "0599000000",
                    BloodType = "O+"
                };

                context.Patients.Add(patient);

                await context.SaveChangesAsync();
            }

            var doctorEmail = "doctor@cardiac.com";
            var doctorPassword = "Doctor@123";

            var doctorUser = await userManager.FindByEmailAsync(doctorEmail);

            if (doctorUser == null)
            {
                doctorUser = new IdentityUser
                {
                    UserName = doctorEmail,
                    Email = doctorEmail
                };

                var doctorUserResult = await userManager.CreateAsync(
                    doctorUser,
                    doctorPassword);

                if (!doctorUserResult.Succeeded)
                    return;
            }

            if (!await userManager.IsInRoleAsync(doctorUser, "Doctor"))
            {
                var doctorRoleResult = await userManager.AddToRoleAsync(
                    doctorUser,
                    "Doctor");

                if (!doctorRoleResult.Succeeded)
                    return;
            }

            var doctor = await context.Doctors
                .FirstOrDefaultAsync(d => d.UserId == doctorUser.Id);

            if (doctor == null)
            {
                doctor = new Doctor
                {
                    UserId = doctorUser.Id,
                    FullName = "Test Doctor",
                    PhoneNumber = "0599111111"
                };

                context.Doctors.Add(doctor);

                await context.SaveChangesAsync();
            }

            var hasVitalSigns = await context.VitalSigns
                .AnyAsync(v => v.PatientId == patient.Id);

            if (!hasVitalSigns)
            {
                var vitalSigns = new List<VitalSign>
                {
                    new VitalSign
                    {
                        PatientId = patient.Id,
                        HeartRate = 72,
                        SystolicBloodPressure = 120,
                        DiastolicBloodPressure = 80,
                        OxygenSaturation = 98,
                        RecordedAt = new DateTime(2026, 8, 10, 9, 0, 0)
                    },

                    new VitalSign
                    {
                        PatientId = patient.Id,
                        HeartRate = 76,
                        SystolicBloodPressure = 118,
                        DiastolicBloodPressure = 78,
                        OxygenSaturation = 97,
                        RecordedAt = new DateTime(2026, 8, 11, 9, 0, 0)
                    }
                };

                context.VitalSigns.AddRange(vitalSigns);
            }

            var hasMedication = await context.Medications
                .AnyAsync(m => m.PatientId == patient.Id);

            if (!hasMedication)
            {
                var medication = new Medication
                {
                    PatientId = patient.Id,
                    Name = "Aspirin",
                    Dosage = "81 mg",
                    Frequency = "Once daily",
                    StartDate = new DateTime(2026, 8, 1),
                    EndDate = null
                };

                context.Medications.Add(medication);
            }

            var hasAppointment = await context.Appointments
                .AnyAsync(a => a.PatientId == patient.Id);

            if (!hasAppointment)
            {
                var appointment = new Appointment
                {
                    PatientId = patient.Id,
                    DoctorId = doctor.Id,
                    AppointmentDate = new DateTime(2026, 9, 1, 10, 0, 0),
                    Reason = "Routine cardiac follow-up",
                    Notes = "Synthetic test appointment"
                };

                context.Appointments.Add(appointment);
            }

            await context.SaveChangesAsync();
        }
    }
}