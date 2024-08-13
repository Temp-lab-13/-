
namespace WATask3.Client
{
    public class LibreryClient : ILibreryClient
    {
        readonly HttpClient _httpClient = new HttpClient();
        public async Task<bool> Exist(Guid id)
        {
            using HttpResponseMessage res = await _httpClient.GetAsync(_httpClient.BaseAddress); // тут какая-то муть с адресом, переписать
            res.EnsureSuccessStatusCode();
            string resCont = await res.Content.ReadAsStringAsync();

            if (resCont == "true") { return true; }
            if (resCont == "false") { return false; }

            throw new Exception("unknow");
        }
    }
}
