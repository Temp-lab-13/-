using WATask.IAbstract;
using WATask.Models.DTO;

namespace WATask.Mutation
{
    public class MiSimpleMutation
    {
        public bool AddProduct(ProductDto productDto, [Service] IServiceProduct serviceProduct) => serviceProduct.AddProduct(productDto);   //  Запрос Графу на добавление продукта.
        public bool AddCategory(CategoryDto categoryDto, [Service] IServiceCategory serviceCategory) => serviceCategory.AddCategory(categoryDto);   //  Запрос Графу на добавление категории.
        public bool DeleteProduct(ProductDto productDto, [Service] IServiceProduct serviceProduct) => serviceProduct.DeletProduct(productDto);  //  Запрос Графу на удаление продукта.
        public bool DeleteCategory(CategoryDto categoryDto, [Service] IServiceCategory serviceCategory) => serviceCategory.DeletCategory(categoryDto);  //  Запрос Графу на удаление категории и всех связанных с ней продуктов.
        public bool UpPrise(ProductDto product, [Service] IServiceProduct serviceProduct) => serviceProduct.UpPrise(product);   //  Запрос Графу на именение цены существуещего продукта.
    }
}
