using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WATask.IAbstract;
using WATask.Models;
using WATask.Models.Context;
using WATask.Models.DTO;

namespace WATask.Service
{
    public class ServiceProduct : IServiceProduct
    {
        private readonly IMapper mapper;
        private IMemoryCache memoryCache;
        private readonly ProductContext context;

        public ServiceProduct(IMapper mapper, IMemoryCache memoryCache, ProductContext context) 
        {
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.context = context;
        }

        public bool AddProduct(ProductDto product)  // Добавялем продукт.
        {
            if (!context.Products.Any(x => x.Name.Equals(product.Name)))
            {
                var entity = mapper.Map<Product>(product);
                context.Products.Add(entity);
                context.SaveChanges();
                memoryCache.Remove("products");
                memoryCache.Remove("productsCSV");
                return true;
            }
            return false;
        }

        public IEnumerable<ProductDto> GetProducts()    //  Получаем списко продукторв.
        {
            if (memoryCache.TryGetValue("products", out List<ProductDto> productsCash))
            {
                return productsCash;
            }

            var products = context.Products.Select(x => mapper.Map<ProductDto>(x)).ToList();
            memoryCache.Set("products", products, TimeSpan.FromMinutes(30));
            return products;
        }

        public ProductDto GetProduct(int productId)     //  Получаем конкретный продукт по id 
        {
            ProductDto product = mapper.Map<ProductDto>(context.Products.Where(x => x.Id == productId).FirstOrDefault());
            return product;
        }

        public bool UpPrise(ProductDto product)     //  Менеям цену у товара.
        {
            if (context.Products.Any(x => x.Name.Equals(product.Name))) // Проверяем, есть ли такой продукт.
            {
                var entity = context.Products.Where(x => x.Name.Equals(product.Name)).FirstOrDefault();
                entity.Price = product.Price;
                context.SaveChanges();
                memoryCache.Remove("products");
                memoryCache.Remove("productsCSV");
                return true;
            }
            return false;
        }

        public bool DeletProduct(ProductDto product)    //  Удаляем продукт.
        {
            if (context.Products.Any(x => x.Name.Equals(product.Name))) // Проверяем, есть ли такой продукт.
            {
                var entity = context.Products.Where(x => x.Name.Equals(product.Name)).FirstOrDefault();
                context.Products.Remove(entity); // Удаяляем его.
                context.SaveChanges(); // Сохраняем изменения.
                memoryCache.Remove("products");
                memoryCache.Remove("productsCSV");
                return true;
            }
            return false ;
        }

        public bool CheckProduct(int productId)     //  Проверяем наличие продукта по его id. Этот метод используется и в других сервесах.
        {
            bool resalt = context.Products.Any(x => x.Id == productId);
            return resalt;
        }

        
    }
}
