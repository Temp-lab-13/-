using WATaskStoreg.IAbstract;
using WATaskStoreg.Models.DTO;

namespace WATaskStoreg.Querty
{
    public class MySimpleQuery
    {
        public IEnumerable<StorageDto> GetProducs([Service] IServiceStorage serviceStorage) => serviceStorage.GetPosition(); // Запрос в Графе на получение списка товаров.
    }
}
