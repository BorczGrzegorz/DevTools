using DevTools.JiraApi.JiraDto;
using System.Linq;
using System.Collections.Generic;
using DevTools.Application.Models;

namespace DevTools.JiraApi.Mock
{
    public class SprintDescriptionResultsDtoBuilder : CollectionBuilder<SprintDescriptionResultsDtoBuilder, SprintDescriptionDto>
    {
        private SprintDescriptionResultsDtoBuilder()
        {}

        public static SprintDescriptionResultsDtoBuilder Empty() => new SprintDescriptionResultsDtoBuilder();
        public static SprintDescriptionResultsDtoBuilder FromSprintDtos(List<SprintDto> sprints)
            => Empty().For(sprints.Count, (i, b) => b.Add(sprints[i].Id, sprints[i].State));

        public new SprintDescriptionResultsDto Build() => new SprintDescriptionResultsDto
        {
            Sprints = Models.ToArray()
        };

        public SprintDescriptionResultsDtoBuilder AddMany(int fromId, int toId, SprintState state)
            => For(toId - fromId, (i) => Add(fromId + i, state));

        public SprintDescriptionResultsDtoBuilder Add(int id, SprintState state = SprintState.CLOSED)
        {
            Add(new SprintDescriptionDto
            {
                Id = id,
                State = state
            });
            return this;
        }
    }
}
