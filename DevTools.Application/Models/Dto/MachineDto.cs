using System;
using System.Collections.Generic;
using System.Text;
using Extensions.Infrastructure.Models;

namespace DevTools.Application.Models.Dto
{
    public class MachineDto
    {
        public MachineId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        public MachineDto(Machine machine)
        {
            Id = machine.Id;
            Name = machine.Name;
            Description = machine.Description;
            Address = machine.Address;
        }
    }
}
