using Newtonsoft.Json;

namespace Justa.Job.Backend.Api.Application.Services.DataValidation.Models
{
    public class EmialValidatorResponse
    {

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("format_valid")]
        public bool FormatValid { get; set; }

        [JsonProperty("smtp_check")]
        public bool SmtpCheck { get; set; }
    }
}