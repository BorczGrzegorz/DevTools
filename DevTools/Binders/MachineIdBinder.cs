using Extensions.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Extensions.Binders
{
    public class MachineIdBinder : IdBinder<MachineId>
    {
        protected override MachineId Parse(Guid id) => new MachineId(id);
    }
}
