
using WATaskStoreg.WebClient.IAbstractClient;

namespace WATaskStoreg.WebClient.Client
{
    public class StoregClient : IStoregClient
    {
        readonly HttpClient Client = new HttpClient();
        public async Task<bool> ExistsProsuct(int id)
        {
            using HttpResponseMessage responseMessage = await Client.GetAsync($"https://localhost:7164/Product/CheckProduct?productId={id.ToString()}");
            responseMessage.EnsureSuccessStatusCode();
            string respond = await responseMessage.Content.ReadAsStringAsync();

            if(respond == "true")
            {
                return true;
            }

            if (respond == "false") 
            {
                return false;
            }

            throw new Exception("Unknow respond");
        }


        /*public async Task<ProductDto> GetProduct(int id) // Предовать объект надо иначе. Как ваирант, либо другой тип месседжера, либо сериализовать/десериализовать и отправлять массивом байт. TODO: Позже реализовать.
        {
            using HttpResponseMessage responseMessage = await Client.GetAsync($"'https://localhost:7164/Product/GetProduct?productId={id.ToString()}");
            var resond = await responseMessage.Content.ReadAsStringAsync();
            return resond;
            throw new NotImplementedException();
        }*/
    }
}
