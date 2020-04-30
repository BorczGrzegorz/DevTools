using Extensions.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTools.Converters
{
    public class MachineIdConverter : IdConverter<MachineId>
    {
        protected override MachineId Create(Guid value) => new MachineId(value);
    }
}
