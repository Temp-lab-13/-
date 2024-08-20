using Microsoft.AspNetCore.Mvc;
using WATask.Models.DTO;

namespace WATask.IAbstract
{
    public interface IServiceProduct
    {
        void AddCategory(CategoryDto category);
        IEnumerable<CategoryDto> GetCategories();
        void AddProduct(ProductDto product);
        IEnumerable<ProductDto> GetProducts();
        void UpPrise(ProductDto product);
        void DeletProduct(ProductDto product);
        void DeletCategory(CategoryDto product);
        string GetProductCsvUrl();
        string GetProductCsv();
        string GetStatistic();
    }
}
