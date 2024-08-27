using Microsoft.AspNetCore.Mvc;
using WATask.IAbstract;

namespace WATask.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IServiceFiles service; // Интерфейс для работы с файлами

        public FileController(IServiceFiles service)
        {
            this.service = service;
        }

        [HttpGet(template: "GetFileCSV")]
        public ActionResult<string> GetFileCSV()    // Получение статичного файла.csv со списком продуктов по ссылке.   
        {
            try
            {
                string path = $"https://{Request.Host.ToString()}/static/{service.GetProductCsvUrl()}";     // Формируем ссылку, получая адрес хоста, название файла. TODO. Чуто переделать что бы получать всё кроме hhtps и адреса хоста.

                return path;                                                                                // Выдаём ссылку клиенту.
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(template: "DownloadFileCSV")]      // Загрузка файла.csv (сам файл нигде не создаётся. Но его можно скачать и сохранить или просто просмотреть.
        public FileContentResult? DownloadFileCSV()
        {
            try
            {
                return File(new System.Text.UTF8Encoding().GetBytes(service.GetProductCsv()), "text/csv", "report.csv");        // Выдаём клиенту файл, сформировав его из результата работы метода(наполнение), указание формата, название файла с его форматом.
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet(template: "GetStatictic")]         // Получение по ссылке статичного файла со статистикой кэша. TODO. Выяснить, можно ли рабить статистику по конкретным категориям?
        public ActionResult<string> GetStatictic()
        {
            try
            {
                string path = $"https://{Request.Host.ToString()}/static/{service.GetStatistic()}";
                return path;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
