using WATask2Storeg.WebClient.Interface;

namespace WATask2Storeg.WebClient
{
    public class StoregClient : IProductClient
    {
        readonly HttpClient client = new HttpClient();
        
        public async Task<bool> ExistProduct(int? productId)
        {
            string query = @"{exist(productId:" + productId + ")}";
            // http://myapi/graphql?query={me{name}}

            using HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7190/GraphQL?query={query}");
            responseMessage.EnsureSuccessStatusCode();
            string response = await responseMessage.Content.ReadAsStringAsync();
            if (response == "true")
            {
                return true;
            }
            if (response == "false") 
            { 
                return false; 
            }

            throw new Exception("хз чё случилось");

            
            
        }
    }
}
