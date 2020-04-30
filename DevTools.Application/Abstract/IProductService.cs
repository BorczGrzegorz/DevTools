using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Models;
using Extensions.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevTools.Application.Abstract
{
    public interface IProductService
    {
        ProductDto Add(string name);
        MachineDto[] AddMachines(ProductId id, NewMachineDto[] machines);
        ProjectDto[] AddProjects(ProductId id, string[] projectsNames);
        AddressDto[] AddAddresses(ProjectId id, NewAddressDto[] newAddresses);
        AddressDto RemoveAddress(ProjectId projectId, AddressId addressId);
        MachineDto RemoveMachine(ProductId productId, MachineId machineId);
        ProjectDto RemoveProject(ProjectId projectId);
    }
}
