using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Exceptions;
using DevTools.Infrastructure.Models;
using Extensions.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevTools.Application.Models
{
    public class Product
    {
        private List<Machine> _machines = new List<Machine>();
        private List<Project> _projects = new List<Project>();
        public ProductId Id { get; private set; }
        public string Name { get; private set; }

        public IReadOnlyCollection<Machine> Machines => _machines.AsReadOnly();
        public IReadOnlyCollection<Project> Projects => _projects.AsReadOnly();

        public Product(string name)
        {
            Name = name;
            Id = ProductId.NewId();
        }

        internal Project[] AddProjects(string[] names)
        {
            if (names == null)
            {
                return null;
            }

            foreach (string name in names)
            {
                AddProject(name);
            }

            return _projects.Skip(_projects.Count - names.Length).ToArray();
        }

        private Project AddProject(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{nameof(name)} is null or empty!");
            }

            Project newProject = new Project(name);
            _projects.Add(newProject);
            return newProject;
        }

        internal Machine[] AddMachines(NewMachineDto[] machines)
        {
            if (machines == null)
            {
                return null;
            }

            foreach (NewMachineDto machineDto in machines)
            {
                AddMachine(machineDto);
            }

            return _machines.Skip(_machines.Count - machines.Length).ToArray();
        }

        internal Address[] AddAddresses(ProjectId projectId, NewAddressDto[] addresses)
        {
            Project project = _projects.SingleOrDefault(x => x.Id == projectId);
            if (project == null)
            {
                throw new ArgumentException($"Project {projectId} does not exist in product {Id}");
            }

            return project.AddAddresses(addresses);
        }

        private Machine AddMachine(NewMachineDto machineDto)
        {
            if (_machines.Any(x => x.Equal(machineDto)))
            {
                throw new ArgumentException($"Machine {machineDto.Name} already exist in product {Name}!");
            }
            Machine newMachine = new Machine(machineDto);
            _machines.Add(newMachine);
            return newMachine;
        }

        internal Machine DeleteMachine(MachineId machineId)
        {
            Machine machineToDelete = _machines.SingleOrDefault(x => x.Id == machineId) 
                                    ?? throw new EntityNotEixtException("machine does not exist!");

            _machines.Remove(machineToDelete);
            return machineToDelete;
        }

        internal Project DeleteProject(ProjectId projectId)
        {
            Project projectToDelete = _projects.SingleOrDefault(x => x.Id == projectId)
                                      ?? throw new ArgumentNullException("project does not exist");

            _projects.Remove(projectToDelete);
            return projectToDelete;
        }
    }
}
