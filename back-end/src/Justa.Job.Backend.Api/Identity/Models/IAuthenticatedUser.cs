using System.Collections.Generic;
using System.Security.Claims;

namespace Justa.Job.Backend.Api.Identity.Models
{
    public interface IAuthenticatedUser
    {
        string Name { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}