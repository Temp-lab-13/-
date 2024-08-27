using WATask2.Interface;
using WATask2.Models.Dto;

namespace WATask2.Query
{
    public class MySimpleQuery
    {
        public IEnumerable<ProductDto> GetProducts([Service] IProductService productService) => productService.GetProducts();
        //public IEnumerable<StoreDto> GetStorage([Service] IStoreService storeService) => storeService.GetStore();
        public IEnumerable<CatalogDto> GetCatlog([Service] ICatalogService catalogService) => catalogService.GetCatalog();
        public bool Exist(int productId, [Service] IProductService productService)
        {
            bool result = productService.ChekProduct(productId);
            return result;
        }

    }
}
