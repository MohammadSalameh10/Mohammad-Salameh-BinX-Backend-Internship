namespace CardiacPatientMonitoringSystem.API.DTOs.Requests
{
    public class LoginRequest
    {
        /// <summary>
        /// The user's email address.
        /// </summary>
        /// <example>patient@cardiac.com</example>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The user's password.
        /// </summary>
        /// <example>Patient@123</example>
        public string Password { get; set; } = string.Empty;
    }
}