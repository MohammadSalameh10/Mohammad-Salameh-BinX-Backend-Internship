namespace CardiacPatientMonitoringSystem.API.DTOs.Responses
{
    public class LoginResponse
    {
        /// <summary>
        /// The JWT access token used to authenticate protected API requests.
        /// </summary>
        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...</example>
        public string Token { get; set; } = string.Empty;
    }
}