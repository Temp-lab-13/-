
using Microsoft.AspNetCore.Mvc;
using WATaskStoreg.IAbstract;
using WATaskStoreg.Models.DTO;
using WATaskStoreg.WebClient.IAbstractClient;

namespace WATaskStoreg.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IServiceStorage service;  // Интерфейс для работы с магазином
        private readonly IStoregClient storegClient;    // Интерфейс для работы с запросами в другие микросервисы. 

        public StorageController(IServiceStorage service, IStoregClient storegClient)
        {
            this.service = service;
            this.storegClient = storegClient;
        }

        [HttpGet(template: "GetPositions")] // Получение позиций.
        public IActionResult GetPosicions()
        {
            var positions = service.GetPosition();
            return Ok(positions);
        }

        // Пример взаимодействия сервеса-магазин с сервесом "всё остальное".  Перед добавление новой позиции в магазине, мы проверяем, что данный товар есть в базе товаров.
        // Если он есть - добавляем, если нет, то не добавляем.
        [HttpPost(template: ("PostPosition"))] // Добавление новой позиции. Пока базовый каркас.
        public async Task AddPosition([FromQuery] int productId, string positionName, string descript, int count) // TODO Реализовать получение самого продукта по id, что бы не заполнять поля в ручную.
        {
            try
            {
                var productExisTask = storegClient.ExistsProsuct(productId);    // Проверка на наличие указанного продукта в базе данных основного приложения. Если продукт есть, то высталяем.
                if (await productExisTask)
                {
                    var position = new StorageDto() { productId = productId, Name = positionName, Descript = descript, Count = count }; // Обязательно указываем id имеющегося продукта.
                    service.AddPosition(position);
                    Ok("Добавлено");
                }
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
        }

        [HttpDelete(template: "DelPosition")]  //Удаление позиции.
        public IActionResult DeletPosicion(string positionName)
        {
            try
            {
                var position = new StorageDto() { Name = positionName };
                service.DeletPosition(position);
                return Ok("Позиция продуктов удалена.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
