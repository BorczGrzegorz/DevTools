using DevTools.Infrastructure.Models;
using System.Linq;

namespace DevTools.Application.Models.Dto
{
    public class ProjectDto
    {
        public ProjectId Id { get; set; }
        public string Name { get; set; }
        public AddressDto[] Addresses { get; set; }

        public ProjectDto(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            Addresses = project.Addresses.Select(x => new AddressDto(x)).ToArray();
        }

        public ProjectDto()
        { }
    }
}
