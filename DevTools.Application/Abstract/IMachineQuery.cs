using DevTools.Application.Models.Dto;
using DevTools.Infrastructure.Models;

namespace DevTools.Application.Abstract
{
    public interface IMachineQuery
    {
        MachineDto[] Get(ProductId productId);
    }
}
