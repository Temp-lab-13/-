using WATask.Models.DTO;

namespace WATask.IAbstract
{
    public interface IServiceCategory
    {
        void AddCategory(CategoryDto category);
        IEnumerable<CategoryDto> GetCategories();
        void DeletCategory(CategoryDto product);
    }
}
