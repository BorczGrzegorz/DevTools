﻿using DevTools.Application.Abstract;
using DevTools.Application.Models.Dto.Issues;
using DevTools.Application.Models.Dto.WorkLog;
using DevTools.Application.Models.SearchParams;
using DevTools.JiraApi.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DevTools.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public class WorkLogControllerV2 : ControllerBase
    {
        private readonly IWorkLogQuery _workLogQuery;
        private readonly IJiraWebClient _client;

        public WorkLogControllerV2(IWorkLogQuery workLogQuery, IJiraWebClient webClient)
        {
            _workLogQuery = workLogQuery ?? throw new ArgumentNullException(nameof(workLogQuery));
            _client = webClient ?? throw new ArgumentNullException(nameof(webClient));
        }

        [HttpGet("issues")]
        public Task<List<IssueDto>> GetIssues([FromQuery] IssueSearchParamsDto searchParamsDto)
        {
            return _workLogQuery.GetIssues(searchParamsDto);
        }

        [HttpGet("users/worklogs")]
        public async Task<ActionResult<UsersWorkLogDto>> GetWorkLogs([FromQuery] SearchParamsDto searchParams)
        {
            var result = await _workLogQuery.GetWorkLogs(searchParams);
            return result;
        }

        [HttpGet("users/issues/worklogs")]
        public async Task<ActionResult<UsersIssueDto>> GetIssues([FromQuery] SearchParamsDto searchParams)
        {
            return await _workLogQuery.GetIssues(searchParams);
        }

        [HttpGet("users/dates/issues")]
        public async Task<ActionResult<UsersDatesSummary>> GetDatesSummary([FromQuery] SearchParamsDto searchParams)
        {
            return await _workLogQuery.GetUserDatesSummary(searchParams);
        }

        [HttpPost("worklogs/issue/{issueId}/minutes/{minutes}")]
        public async Task<IActionResult> LogWork([FromRoute]string issueId, [FromRoute]int minutes)
        {
            await _client.LogWork(issueId, minutes);
            return Ok();
        }
    }
}
