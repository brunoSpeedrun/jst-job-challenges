using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Requests
{
    public class QueryUserGetPage : IRequest<IActionResult>
    {
        private int _pageSize = 10;
        public const int MaxPageSize = 50;
        public int Page { get; set; } = 0;
    
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }

        public string SortBy { get; set; }

        [RegularExpression("asc|desc", ErrorMessage = "SortDirection must be 'asc' or 'desc'")]
        public string SortDirection { get; set; } = "asc";

        public string Search { get; set; }
    }
}