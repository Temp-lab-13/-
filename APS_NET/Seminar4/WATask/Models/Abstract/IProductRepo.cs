using WATask.Models.Dto;

namespace WATask.Models.Abstract
{
    public interface IProductRepo
    {
        public int AddCatalog(CatalogDto catalogDto);
        public int AddProduct(ProductDto productDto);
        public IEnumerable<ProductDto> GetProducts();
        public IEnumerable<CatalogDto> GetCategories();
    }
}
