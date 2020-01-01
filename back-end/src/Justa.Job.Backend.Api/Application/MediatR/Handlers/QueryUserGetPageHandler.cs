using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using Justa.Job.Backend.Api.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Justa.Job.Backend.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;

namespace Justa.Job.Backend.Api.Application.MediatR.Handlers
{
    public class QueryUserGetPageHandler : ActionResultRequestHandler<QueryUserGetPage>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpContext _httpContext;

        public QueryUserGetPageHandler(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public override async Task<IActionResult> Handle(QueryUserGetPage request, CancellationToken cancellationToken)
        {
            var queryUser = _userManager.Users;

            if (!string.IsNullOrEmpty(request.Search))
            {
                queryUser = queryUser.Where(q => q.UserName.Contains(request.Search) ||
                                                 q.Email.Contains(request.Search) ||
                                                 q.PhoneNumber.Contains(request.Search));
            }

            if (!string.IsNullOrEmpty(request.SortBy) && 
                typeof(ApplicationUser).GetProperties().Any(p => string.Equals(p.Name, request.SortBy, StringComparison.InvariantCultureIgnoreCase)))
            {
                queryUser = request.SortDirection == "asc"
                                ? queryUser.OrderBy(request.SortBy)
                                : queryUser.OrderByDescending(request.SortBy);
            }

            var skip = request.Page * request.PageSize;

            queryUser = queryUser.Skip(skip)
                                 .Take(request.PageSize);

            var users = await queryUser.Select(q => new 
            {
                q.Id,
                q.UserName,
                q.Email,
                q.PhoneNumber
            })
            .ToListAsync();

            var dataWithHateoas = users.Select(u => new
            {
                u.Id,
                u.UserName,
                u.Email,
                u.PhoneNumber,
                _links = new [] {
                    new {
                        rel = "self",
                        href = $"{_httpContext.GetAppBaseUrl()}/users/{u.UserName}",
                        method = "GET"
                    },
                    new {
                        rel = "update-user",
                        href = $"{_httpContext.GetAppBaseUrl()}/users/{u.UserName}",
                        method = "PUT"
                    },
                    new {
                        rel = "delete-user",
                        href = $"{_httpContext.GetAppBaseUrl()}/users/{u.UserName}",
                        method = "DELETE"
                    }
                }
            })
            .ToList();

            return Ok(dataWithHateoas);
        }
    }
}