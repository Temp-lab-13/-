using WATask2Storeg.Interface;
using WATask2Storeg.Models.Dto;

namespace WATask2Storeg.Mutatin
{
    public class MiSimpleMutation
    {

        public int AddPosition(StoreDto store, [Service] IStoreService service) 
        {
            var id = service.AddPosition(store);
            return id; 
        }
    }
}
