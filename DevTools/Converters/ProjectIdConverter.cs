using DevTools.Infrastructure.Models;
using System;

namespace DevTools.Converters
{
    public class ProjectIdConverter : IdConverter<ProjectId>
    {
        protected override ProjectId Create(Guid value) => new ProjectId(value);
    }
}
