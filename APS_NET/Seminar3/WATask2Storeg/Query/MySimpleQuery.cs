using WATask2Storeg.Interface;
using WATask2Storeg.Models.Dto;

namespace WATask2Storeg.Query
{
    public class MySimpleQuery
    {
        public IEnumerable<StoreDto> GetPosition([Service] IStoreService storeService) => storeService.GetPosition();
    }
}
