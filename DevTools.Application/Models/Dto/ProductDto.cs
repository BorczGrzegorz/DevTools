using DevTools.Infrastructure.Models;
using System.Linq;

namespace DevTools.Application.Models.Dto
{
    public class ProductDto
    {
        public ProductId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MachineDto[] Machines { get; set; }
        public ProjectDto[] Projects { get; set; }
        public ProductDto()
        {}

        public ProductDto(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            if (product.Machines != null)
            {
                Machines = product.Machines.Select(x => new MachineDto(x)).ToArray();
            }
            if(product.Projects != null)
            {
                Projects = product.Projects.Select(x => new ProjectDto(x)).ToArray();
            }
        }
    }
}
