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

            string[] roles = { "Admin", "Patient" };

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

            var vitalSignCount = await context.VitalSigns
                .CountAsync(v => v.PatientId == patient.Id);

            if (vitalSignCount < 50)
            {
                var vitalSigns = new List<VitalSign>();

                for (int i = vitalSignCount; i < 50; i++)
                {
                    vitalSigns.Add(new VitalSign
                    {
                        PatientId = patient.Id,
                        HeartRate = 65 + (i % 35),
                        SystolicBloodPressure = 110 + (i % 20),
                        DiastolicBloodPressure = 70 + (i % 15),
                        OxygenSaturation = 95 + (i % 5),
                        RecordedAt = new DateTime(2026, 8, 1)
                            .AddHours(i * 6)
                    });
                }

                context.VitalSigns.AddRange(vitalSigns);
            }

            var medicationCount = await context.Medications
                .CountAsync(m => m.PatientId == patient.Id);

            if (medicationCount < 50)
            {
                var medications = new List<Medication>();

                for (int i = medicationCount; i < 50; i++)
                {
                    medications.Add(new Medication
                    {
                        PatientId = patient.Id,
                        Name = $"Medication {i + 1}",
                        Dosage = $"{50 + (i % 5) * 25} mg",
                        Frequency = i % 2 == 0 ? "Once daily" : "Twice daily",
                        StartDate = new DateTime(2026, 8, 1).AddDays(i),
                        EndDate = i % 3 == 0
                            ? new DateTime(2026, 10, 1).AddDays(i)
                            : null
                    });
                }

                context.Medications.AddRange(medications);
            }

            var appointmentCount = await context.Appointments
                .CountAsync(a => a.PatientId == patient.Id);

            if (appointmentCount < 50)
            {
                var appointments = new List<Appointment>();

                for (int i = appointmentCount; i < 50; i++)
                {
                    appointments.Add(new Appointment
                    {
                        PatientId = patient.Id,
                        AppointmentDate = new DateTime(2026, 9, 1, 10, 0, 0)
                            .AddDays(i),
                        Reason = $"Cardiac follow-up {i + 1}",
                        Notes = $"Synthetic appointment record {i + 1}"
                    });
                }

                context.Appointments.AddRange(appointments);
            }

            await context.SaveChangesAsync();
        }
    }
}