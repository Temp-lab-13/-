using WATask2.Models.Dto;

namespace WATask2.Interface
{
    public interface IProductService
    {
        public int AddProduct(ProductDto product);
        public IEnumerable<ProductDto> GetProducts();
    }
}
