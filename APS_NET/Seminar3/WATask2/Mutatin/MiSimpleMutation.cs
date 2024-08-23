using WATask2.Interface;
using WATask2.Models.Dto;

namespace WATask2.Mutatin
{
    public class MiSimpleMutation
    {
        public int AddProduct(ProductDto product, [Service] IProductService service)
        {
            var id = service.AddProduct(product);
            return id;
        }

        public int AddCategory(CatalogDto catalog, [Service] ICatalogService service) 
        {
            var id = service.AddCatalog(catalog);
            return id; 
        }
    }
}
