using DevTools.Infrastructure.Models;
using Extensions.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.Binders
{
    public class ProjectIdBinder : IdBinder<ProjectId>
    {
        protected override ProjectId Parse(Guid id) => new ProjectId(id);
    }
}
