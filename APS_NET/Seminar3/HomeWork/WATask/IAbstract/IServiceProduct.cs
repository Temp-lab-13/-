using Microsoft.AspNetCore.Mvc;
using WATask.Models.DTO;

namespace WATask.IAbstract
{
    public interface IServiceProduct
    {
        void AddProduct(ProductDto product);
        IEnumerable<ProductDto> GetProducts();
        void UpPrise(ProductDto product);
        void DeletProduct(ProductDto product);
    }
}
