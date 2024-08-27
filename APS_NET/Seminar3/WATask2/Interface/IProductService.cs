using WATask2.Models.Dto;

namespace WATask2.Interface
{
    public interface IProductService
    {
        int AddProduct(ProductDto product);
        IEnumerable<ProductDto> GetProducts();
        bool ChekProduct(int productId);
    }
}
