using DevTools.Application.Abstract;
using DevTools.Application.Models.Dto.Issues;
using DevTools.Application.Models.SearchParams;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.Controllers
{
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IWorkLogQuery _workLogQuery;
        public IssueController(IWorkLogQuery workLogQuery)
        {
            _workLogQuery = workLogQuery ?? throw new ArgumentNullException(nameof(workLogQuery));
        }

        [HttpGet("board/{boardId}/issues")]
        public Task<List<IssueDto>> GetIssues([FromQuery] IssueSearchParamsDto searchParamsDto)
        {
            return _workLogQuery.GetIssues(searchParamsDto);
        }
    }
}
