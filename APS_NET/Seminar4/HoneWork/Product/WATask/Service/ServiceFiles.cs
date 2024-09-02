using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using WATask.IAbstract;
using WATask.Models.Context;
using WATask.Models.DTO;

namespace WATask.Service
{
    public class ServiceFiles : IServiceFiles
    {
        private readonly IMapper mapper;
        private IMemoryCache memoryCache;
        private readonly ProductContext context;

        public ServiceFiles(IMapper mapper, IMemoryCache memoryCache, ProductContext context)
        {
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.context = context;
        }

        public string GetProductCsvUrl()    // Запись и получение по ссылке статического файла со списком продуктов.
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
            File.WriteAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory(), directoryName, fileName), content);
            return fileName;
        }

        public string GetProductCsv()   // Получение файла со списком продуктов для скачивания.
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

        public string GetStatistic() // Запись и получение по ссылке статического файла со статистикой отработки кэша.
        {
            var statistic = GetCach(memoryCache.GetCurrentStatistics());
            string statisticFileName = "CachStat.csv";
            string directoryName = "StaticFiles";
            File.WriteAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory(), directoryName, statisticFileName), statistic);
            return statisticFileName;
        }

        private string GetCsv(IEnumerable<ProductDto> products) // Вспомогательный метод для формирование записей в файлы csv, для продуктов.
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in products)
            {
                sb.AppendLine(($"Id: {item.Id}; Name: {item.Name}; Descript: {item.Descript}; Price: {item.Price}; CategoriId: {item.CategoriId};"));
            }
            return sb.ToString();
        }

        private string GetCach(MemoryCacheStatistics statistics) // Вспомогательный метод для формирование записей в файлы csv, для кэша.
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(($"Entry Count: {statistics.CurrentEntryCount.ToString()}, EstimatedSize: {statistics.CurrentEstimatedSize.ToString()}, TotalHits: {statistics.TotalHits.ToString()}, TotalMisses: {statistics.TotalMisses.ToString()}"));
            return sb.ToString();
        }
    }
}
