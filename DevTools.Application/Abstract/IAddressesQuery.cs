using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Models;

namespace DevTools.Application.Abstract
{
    public interface IAddressesQuery
    {
        AddressDto[] GetAddresses(ProjectId projectId);
    }
}
