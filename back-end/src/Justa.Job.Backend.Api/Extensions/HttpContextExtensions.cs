using Microsoft.AspNetCore.Http;

namespace Justa.Job.Backend.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetAppBaseUrl(this HttpContext httpContext)
            => $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}";
    }
}