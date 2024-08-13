using WATask2.Models.Dto;

namespace WATask2.Models.Abstract
{
    public interface IProductRepo
    {
        public int AddCatalog(CatalogDto catalogDto);
        public int AddProduct(ProductDto productDto);
        public IEnumerable<ProductDto> GetProducts();
        public IEnumerable<CatalogDto> GetCategories();
    }
}
