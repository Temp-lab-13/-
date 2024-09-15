using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeWork.Serializations
{
    // Класс для сериализации.
    public class SerialazJson
    {
        // Передаём в метод акой-то готовый объект, и сериализуем в JSON, возращая строку.
        public string SerialazToJson(object T)
        {
            string jsonFile = "";
            // Проверяем, что нам передали хоть что-то.
            if (T is not null)
            {
                // Собираем опцию, для читаемого отображения результата со всеми пробелами и переносами.
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                // на случай, если с переданным объектом что-то не так и сериализация не возможна, оборачивает в отлавливатель ошибок.
                try
                {
                    // Сериализуем.
                    jsonFile = JsonSerializer.Serialize(T, options);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Объект не удалось среилаизовать");
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Для сериализации переданны не полные данные");
            }

            // Возращаем результат. (я не стал для этой работы использовать сохраниния в файлы.)
            return jsonFile;
        }
    }
}
