namespace Justa.Job.Backend.Api.Application.Services.DataValidation.Models
{
    public class ValidatorResponse
    {
        public string Type { get; set; }
        public bool IsValid { get; set; }
        public string Value { get; set; }
        public string Formated { get; set; }
    }
}