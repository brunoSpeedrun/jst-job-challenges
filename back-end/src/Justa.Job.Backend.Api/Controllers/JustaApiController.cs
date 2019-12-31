using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class JustaApiController : ControllerBase
    {
        
    }
}