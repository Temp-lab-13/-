using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WATask.IAbstract;
using WATask.Models;
using WATask.Models.Context;
using WATask.Models.DTO;

namespace WATask.Service
{
    public class ServiceCategory : IServiceCategory
    {
        private readonly IMapper mapper;
        private IMemoryCache memoryCache;
        private readonly ProductContext context;

        public ServiceCategory(IMapper mapper, IMemoryCache memoryCache, ProductContext context)
        {
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.context = context;
        }

        public bool AddCategory(CategoryDto category)    // Добавление категории.
        {
            if (!context.Categories.Any(x => x.Name.Equals(category.Name)))
            {
                var entity = mapper.Map<Category>(category);
                context.Categories.Add(entity);
                context.SaveChanges();
                memoryCache.Remove("categorys");
                return true;
            }
            return false;
        }

        public IEnumerable<CategoryDto> GetCategories() // Получение категории.
        {
            if (memoryCache.TryGetValue("categorys", out List<CategoryDto> categoriesCash))
            {
                return categoriesCash;
            }

            var categorys = context.Categories.Select(x => mapper.Map<CategoryDto>(x)).ToList();
            memoryCache.Set("categorys", categorys, TimeSpan.FromMinutes(30));
            return categorys;
        }

        public bool DeletCategory(CategoryDto category) // Удаляем категории.
        {
            if (context.Categories.Any(x => x.Name.Equals(category.Name))) // Проверяем, есть ли такая категория.
            {
                var entity = context.Categories.Where(x => x.Name.Equals(category.Name)).FirstOrDefault();
                var groupProduct = context.Products.Where(x => x.Id.Equals(category.Id)).ToList();
                if (groupProduct.Any()) context.Products.RemoveRange(groupProduct); // Удаляем товары, предварительно проверив, что в категории хоть что=то есть.
                context.Categories.Remove(entity); // Удаялем Группу.
                context.SaveChanges(); // Сохраняем изменения.
                memoryCache.Remove("categorys");    // Чистим кэши.
                memoryCache.Remove("products");
                memoryCache.Remove("productsCSV");
                return true;
            }
            return false;
        }

    }
}
