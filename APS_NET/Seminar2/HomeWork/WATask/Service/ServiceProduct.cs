using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using System.Text.Json;
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
        public void AddCategory(CategoryDto category)
        {
            if (!context.Categories.Any(x => x.Name.Equals(category.Name)))
            {
                var entity = mapper.Map<Category>(category);
                context.Categories.Add(entity);
                context.SaveChanges();
                memoryCache.Remove("categorys");
            }
        }

        public void AddProduct(ProductDto product)
        {
            if (!context.Products.Any(x => x.Name.Equals(product.Name)))
            {
                var entity = mapper.Map<Product>(product);
                context.Products.Add(entity);
                context.SaveChanges();
                memoryCache.Remove("products");
                memoryCache.Remove("productsCSV");
            }
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            if (memoryCache.TryGetValue("categorys", out List<CategoryDto> categoriesCash))
            {
                return categoriesCash;
            } 

            var categorys = context.Categories.Select(x => mapper.Map<CategoryDto>(x)).ToList();
            memoryCache.Set("categorys", categorys, TimeSpan.FromMinutes(30));
            return categorys;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            if (memoryCache.TryGetValue("products", out List<ProductDto> productsCash))
            {
                return productsCash;
            }

            var products = context.Products.Select(x => mapper.Map<ProductDto>(x)).ToList();
            memoryCache.Set("products", products, TimeSpan.FromMinutes(30));
            return products;
        }

        public void UpPrise(ProductDto product)
        {
            if (context.Products.Any(x => x.Name.Equals(product.Name))) // Проверяем, есть ли такой продукт.
            {
                var entity = context.Products.Where(x => x.Name.Equals(product.Name)).FirstOrDefault();
                entity.Price = product.Price;
                context.SaveChanges();
                memoryCache.Remove("products");
            }
        }

        public void DeletProduct(ProductDto product)
        {
            if (context.Products.Any(x => x.Name.Equals(product.Name))) // Проверяем, есть ли такой продукт.
            {
                var entity = context.Products.Where(x => x.Name.Equals(product.Name)).FirstOrDefault();
                context.Products.Remove(entity); // Удаяляем его.
                context.SaveChanges(); // Сохраняем изменения.
                memoryCache.Remove("products");
            }
        }

        public void DeletCategory(CategoryDto category)
        {
            if (context.Categories.Any(x => x.Name.Equals(category.Name))) // Проверяем, есть ли такая категория.
            {
                var entity = context.Categories.Where(x => x.Name.Equals(category.Name)).FirstOrDefault();
                var groupProduct = context.Products.Where(x => x.Id.Equals(category.Id)).ToList();
                if (groupProduct.Any()) context.Products.RemoveRange(groupProduct); // Удаляем товары, предварительно проверив, что в категории хоть что=то есть.
                context.Categories.Remove(entity); // Удаялем Группу.
                context.SaveChanges(); // Сохраняем изменения.
                memoryCache.Remove("categorys");
                memoryCache.Remove("products");
            }
        }

        public string GetProductCsvUrl()
        {
            var content = "";
            if (memoryCache.TryGetValue("productsCSV", out List<ProductDto> productsCash))
            {
                content = GetCsv(productsCash);
            }
            else
            {
                var products = context.Products.Select(x => mapper.Map<ProductDto>(x)).ToList();
                content = GetCsv(products);
                memoryCache.Set("productsCSV", products, TimeSpan.FromMinutes(30));
            }
            
            string fileName = "Product_list.csv";
            string statisticFileName = "CachStat.txt";
            string directoryName = "StaticFiles";
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), directoryName, fileName), content);
            return fileName;
        }

        public string GetProductCsv()
        {
            var content = "";
            if (memoryCache.TryGetValue("productsCSV", out List<ProductDto> productsCash))
            {
                content = GetCsv(productsCash);
            }
            else
            {
                var products = context.Products.Select(x => mapper.Map<ProductDto>(x)).ToList();
                content = GetCsv(products);
                memoryCache.Set("productsCSV", products, TimeSpan.FromMinutes(30));
            }
            return content;
        }

        public string GetStatistic()
        {
            var statistic = GetCach(memoryCache.GetCurrentStatistics());
            string statisticFileName = "CachStat.csv";
            string directoryName = "StaticFiles";
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), directoryName, statisticFileName), statistic);
            return statisticFileName;
        }



        private string GetCsv(IEnumerable<ProductDto> products)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in products)
            {
                sb.AppendLine(($"Id: {item.Id}; Name: {item.Name}; Descript: {item.Descript}; Price: {item.Price}; CategoriId: {item.CategoriId};"));
            }
            return sb.ToString();
        }

        private string GetCach(MemoryCacheStatistics statistics)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(($"Entry Count: {statistics.CurrentEntryCount.ToString()}, EstimatedSize: {statistics.CurrentEstimatedSize.ToString()}, TotalHits: {statistics.TotalHits.ToString()}, TotalMisses: {statistics.TotalMisses.ToString()}"));
            return sb.ToString();
        }
    }
}
