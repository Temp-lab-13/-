using WATask2.Models.Dto;

namespace WATask2.Interface
{
    public interface ICatalogService
    {
        public int AddCatalog(CatalogDto catalog);
        public IEnumerable<CatalogDto> GetCatalog();
    }
}
