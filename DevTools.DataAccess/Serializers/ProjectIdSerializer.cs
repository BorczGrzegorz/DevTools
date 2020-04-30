using DevTools.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.DataAccess.Serializers
{
    internal class ProjectIdSerializer : IdSerializer<ProjectId>
    {
        protected override ProjectId Convert(Guid? value) => (ProjectId)value;

        protected override Guid? Convert(ProjectId model) => (Guid?)model;
    }
}
