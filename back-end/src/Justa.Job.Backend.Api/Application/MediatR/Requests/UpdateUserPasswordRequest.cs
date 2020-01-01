using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Requests
{
    public class ChangeUserPasswordRequest : IRequest<IActionResult>
    {
        [JsonIgnore]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "CurrentUserPassword is required.")]
        [StringLength(100, ErrorMessage = "CurrentUserPassword Must be between 6 and 100 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string CurrentUserPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "NewPassword is required.")]
        [StringLength(100, ErrorMessage = "NewPassword Must be between 6 and 100 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}