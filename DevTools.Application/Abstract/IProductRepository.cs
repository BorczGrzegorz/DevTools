using DevTools.Application.Models;
using DevTools.Infrastructure.Models;

namespace DevTools.Application.Abstract
{
    public interface IProductRepository
    {
        void Save(Product product);
        bool Exist(string name);
        Product Get(ProductId productId);
        Product Get(ProjectId projectId);
        void Update(Product product);
    }
}
