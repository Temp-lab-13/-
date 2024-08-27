using WATask.Models.DTO;

namespace WATask.IAbstract
{
    public interface IServiceCategory
    {
        bool AddCategory(CategoryDto category); // Методы буленвы потому то Граф требует у мутаций методов возращаяющий хоть что-то. Ну и так профе понимать что не сработало что-то.
        IEnumerable<CategoryDto> GetCategories();
        bool DeletCategory(CategoryDto product);
    }
}
