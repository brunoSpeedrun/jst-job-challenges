using Justa.Job.Backend.Api.Application.Services.Jwt.Models;
using Justa.Job.Backend.Api.Identity.Models;

namespace Justa.Job.Backend.Api.Application.Services.Jwt.Interfaces
{
    public interface IJwtService
    {
         JsonWebToken CreateJsonWebToken(ApplicationUser user);
    }
}