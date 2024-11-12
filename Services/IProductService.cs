using ProductSpace.Models;
using ProductSpace.Services.DTOs;

namespace ProductSpace.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();

        Product CreateProduct(CreateProductRequest request);

        bool DeleteProduct(int id);

    }
}
