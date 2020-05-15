using DevTools.Application.Models;
using DevTools.Application.Models.Dto.Issues;
using DevTools.Application.Models.Dto.WorkLog;
using DevTools.Application.Models.SearchParams;
using DevTools.Application.Models.SearchParams.Abstract;
using DevTools.JiraApi.JiraDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.JiraApi
{
    public static class JiraQueryExtensions
    {
        public static int[] FilterSprintIds(this SprintDescriptionResultsDto source, SearchParamsDto searchParams)
            => source.Sprints.WhereIf(searchParams.SprintState != null, x => x.State == searchParams.SprintState).Select(x => x.Id).ToArray();
        public static IEnumerable<IGrouping<string, JiraWorkLogDto>> GroupByAuthor(this IEnumerable<JiraWorkLogDto> source) => source.GroupBy(x => x.Author.Key);
        public static IEnumerable<JiraWorkLogDto> FlatMap(this IEnumerable<JiraWorkLogDto>[] source) => source.SelectMany(x => x);
        public static IEnumerable<JiraWorkLogDto> FilterByUsers(this IEnumerable<JiraWorkLogDto> source, 
                                                                IUserFilter searchParams)
            => source.WhereIf(searchParams.UserName != null && searchParams.UserName.Any(), x => searchParams.UserName.Contains(x.Author.Key));

        public static IEnumerable<JiraWorkLogDto> FilterByStartFromAndSrpintDates<T>(this IEnumerable<JiraWorkLogDto> source,
                                                                                  T searchParams,
                                                                                  IEnumerable<SprintDto> sprints)
            where T : IDateFilter, ISprintFilter
        {
            if (searchParams.StartFrom.HasValue)
            {
                return source.FilterByDate(searchParams);
            }

            if (searchParams.SprintState != null)
            {
                return source.FilterBySprintDates(searchParams, sprints);
            }

            return source.FilterByDate(searchParams.GetDefaultStartFrom());
        }

        public static IEnumerable<JiraWorkLogDto> FilterBySprintDates(this IEnumerable<JiraWorkLogDto> source,
                                                                     ISprintFilter searchParams,
                                                                     IEnumerable<SprintDto> sprints)
        {
            if (!sprints.Any())
            {
                return source;
            }

            SprintDto firstSprint = sprints.OrderBy(x => x.StartDate).First();
            return source.Where(x => x.FilterAfterStartDate(firstSprint));
        }

        public static IEnumerable<JiraWorkLogDto> FilterByDate(this IEnumerable<JiraWorkLogDto> source, 
                                                               IDateFilter searchParams)
            => source.WhereIf(searchParams.StartFrom.HasValue, x => x.FilterAfterDate(searchParams.StartFrom.Value));

        public static IEnumerable<JiraWorkLogDto> FilterByDate(this IEnumerable<JiraWorkLogDto> source, DateTime date)
            => source.Where(x => x.FilterAfterDate(date));
        public static async Task<IEnumerable<SprintDto>> FilterByDate(this IEnumerable<SprintDescriptionDto> source,
                                                                      DateTime startFrom,
                                                                      Func<SprintDescriptionDto, Task<SprintDto>> getSprint)
        {
            List<SprintDto> result = new List<SprintDto>();
            source = source.Reverse().ToList();

            foreach (SprintDescriptionDto sprintDescriptionDto in source)
            {
                SprintDto sprint = await getSprint(sprintDescriptionDto);
                if ((startFrom <= sprint.EndDate))
                {
                    result.Add(sprint);
                    continue;
                }

                break;
            }

            return result;
        }

        public static IEnumerable<T> FilterState<T>(this IEnumerable<T> source, ISprintFilter searchParamsDto, Func<T, SprintState, bool> filter)
            => source.WhereIf(searchParamsDto.SprintState != null, x => filter(x, searchParamsDto.SprintState.Value));

        private static bool FilterAfterStartDate(this JiraWorkLogDto workLog, SprintDto sprint)
        {
            DateTimeOffset dateOffset = new DateTimeOffset(sprint.StartDate, workLog.Started.Offset);
            return workLog.Started >= dateOffset;
        }

        private static bool FilterAfterDate(this JiraWorkLogDto workLog, DateTime date)
        {
            return workLog.Started > date;
        }

        public static UsersIssueDto ToIssueWorklogDictionary(this IEnumerable<IGrouping<string, JiraWorkLogDto>> source,
                                                                                                IEnumerable<JiraIssueDto> issues)
        {
            var dictionary = source.ToDictionary(x => x.Key, y => issues.Select(z => new IssueWorkLogDto
            {
                Id = z.Id,
                Key = z.Key,
                Summary = z.Fields.Summary,
                Worklogs = y.Where(x => x.IssueId == z.Id).Select(w => new WorkLogDto
                {
                    Id = w.Id,
                    IssueId = w.IssueId,
                    Created = w.Started.DateTime,
                    TimeSpentSeconds = w.TimeSpentSeconds
                })
                .OrderBy(x => x.Created)
                .ToList()
            }));

            return new UsersIssueDto(dictionary);
        }

        public static UsersDatesSummary ToDateIssueSummaryDictionary(this IEnumerable<IGrouping<string, JiraWorkLogDto>> source,
                                                                                            IEnumerable<JiraIssueDto> issues)
        {
            var dictionary = source.ToDictionary(x => x.Key, y => new DatesIssueSummaryDto(
                                           y.OrderBy(x => x.Started)
                                            .GroupBy(z => new DateTime(z.Started.Year, z.Started.Month, z.Started.Day))
                                            .ToDictionary(k => k.Key,
                                                          v => v.GroupBy(x => x.IssueId)
                                                                .Select(x => new IssueSummaryDto
                                                                {
                                                                    IssueId = x.Key,
                                                                    TimeSpentSeconds = x.Sum(worklog => worklog.TimeSpentSeconds),
                                                                    Key = issues.Single(issue => issue.Id == x.Key).Key,
                                                                    Summary = issues.Single(issue => issue.Id == x.Key).Fields.Summary
                                                                }).ToList()
                                           )));

            return new UsersDatesSummary(dictionary);
        }

        public static UsersWorkLogDto ToUsersWorkLogDto(this IEnumerable<IGrouping<string, JiraWorkLogDto>> source)
        {
            var dictionary = source.ToDictionary(x => x.Key, y => y.OrderBy(x => x.Started).Select(z => new WorkLogDto()
            {
                Created = z.Started.DateTime,
                Id = z.Id,
                IssueId = z.IssueId,
                TimeSpentSeconds = z.TimeSpentSeconds
            }));

            return new UsersWorkLogDto(dictionary);
        }
    }
}
