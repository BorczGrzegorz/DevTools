using DevTools.Application.Models.Dto;
using Extensions.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Application.Models
{
    public class Machine
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Address { get; private set; }
        public MachineId Id { get; private set; }

        public Machine(string name, string address, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Description = description;
            Id = MachineId.NewId();
        }

        public Machine(NewMachineDto newMachineDto) : this(newMachineDto.Name, 
                                                           newMachineDto.Address, 
                                                           newMachineDto.Description)
        { }

        public override bool Equals(object obj)
        {
            var machine = obj as Machine;
            return machine != null &&
                   Name?.ToLower() == machine.Name?.ToLower();
        }

        public bool Equal(NewMachineDto newMachineDto)
        {
            if (newMachineDto == null)
            {
                return false;
            }

            return newMachineDto.Name?.ToLower() == Name?.ToLower();
        }

        public override int GetHashCode()
        {
            var hashCode = -616372265;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<MachineId>.Default.GetHashCode(Id);
            return hashCode;
        }
    }
}
