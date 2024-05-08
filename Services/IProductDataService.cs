using Ghostchef.Models;

namespace Ghostchef.Services
{
    public interface IProductDataService
    {
        int Create(Product product);
        List<Product> GetAll();
        Product? Read(int id);
        bool Update(int id, Product product);
        bool Delete(int id);
    }
}
