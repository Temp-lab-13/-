using WATask.IAbstract;
using WATask.Models.DTO;

namespace WATask.Querty
{
    public class MySimpleQuery
    {
        public IEnumerable<ProductDto> GetProducs([Service] IServiceProduct serviceProduct) => serviceProduct.GetProducts();    //  Запрос Графу на получение списка продуктов.
        public ProductDto GetProduc(int productId, [Service] IServiceProduct serviceProduct) => serviceProduct.GetProduct(productId);   //  Запрос Графу на получение конкретного продукта.

        public bool CheckProducs(int productId, [Service] IServiceProduct serviceProduct) => serviceProduct.CheckProduct(productId);    //  Запрос Графу на проверку наличия продукта.
        public IEnumerable<CategoryDto> GetCatlog([Service] IServiceCategory serviceCategory ) => serviceCategory.GetCategories();      //  Запрос Графу на получение списка категорий продуктов..

    }
}
