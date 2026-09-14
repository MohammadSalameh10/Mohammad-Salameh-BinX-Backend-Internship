namespace CardiacPatientMonitoringSystem.API.DTOs.Requests
{
    public class RegisterRequest
    {
        /// <summary>
        /// The patient's email address.
        /// </summary>
        /// <example>patient@cardiac.com</example>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The patient's password.
        /// </summary>
        /// <example>Patient@123</example>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// The patient's full name.
        /// </summary>
        /// <example>Mohammad Ahmad</example>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// The patient's date of birth.
        /// </summary>
        /// <example>1998-05-15T00:00:00</example>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// The patient's gender.
        /// </summary>
        /// <example>Male</example>
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// The patient's phone number.
        /// </summary>
        /// <example>0599123456</example>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// The patient's blood type.
        /// </summary>
        /// <example>O+</example>
        public string BloodType { get; set; } = string.Empty;
    }
}