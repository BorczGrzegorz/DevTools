using DevTools.Application.Models;
using DevTools.JiraApi.JiraDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevTools.JiraApi.Mock
{
    public class SprintDtoCollectionBuilder : CollectionBuilder<SprintDtoCollectionBuilder, SprintDto>
    {
        private SprintDtoCollectionBuilder()
        { }

        public static SprintDtoCollectionBuilder Empty() => new SprintDtoCollectionBuilder();

        public SprintDtoCollectionBuilder AddMany(int fromId, int toId, TimeSpan sprintRange, SprintState state = SprintState.CLOSED)
            => For(toId - fromId, (i, b) => Add(fromId + i, sprintRange, state));

        public SprintDtoCollectionBuilder Add(int id, TimeSpan sprintRange, SprintState sprintState = SprintState.CLOSED)
        {
            DateTime endDate = DateTime.UtcNow;
            if (Models.Any())
            {
                endDate = Models.Last().StartDate;
            }

            return Add(id, endDate.Subtract(sprintRange), endDate, sprintState);
        }

        public SprintDtoCollectionBuilder AddActive(int id,
                                                    DateTime startDate,
                                                    DateTime endDate)
        => Add(id, startDate, endDate, SprintState.ACTIVE);


        public SprintDtoCollectionBuilder Add(int id,
                                              DateTime startDate,
                                              DateTime endDate,
                                              SprintState state = SprintState.CLOSED)
        {
            Add(new SprintDto
            {
                Id = id,
                StartDate = startDate,
                EndDate = endDate,
                State = state
            });
            return this;
        }
    }
}
