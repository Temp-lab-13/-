using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HomeWork.Converts
{
    public class ConvertToXML
    {
        // Класс конвертирует строку JSON-формата в строку XML-формата.
        // Мы должны получить само строку и корректный тип класса, с которым работаем.
        public string ConvertXML(string stringJSON, Type type)
        {
            string stringXML = "";
            // Проверяем, что нам передали данные обоих типов. 
            if (stringJSON is not null && type is not null)
            {
                // На случай, если переданн не коректный тип класса, или косяк с JSON-строкой, устанавливаем ловушку на ошибки.
                try
                {
                    // В задании, было указано испольованание JsonDocument, но я не совсем понимаю, что с ним делать то. о_0"
                    // Поэтому, просто испольую его для десериализации,
                    // а так, как его надо закрывать, используем юзинг.
                    using (JsonDocument doc = JsonDocument.Parse(stringJSON))
                    {
                        var temp = doc.Deserialize(type);
                        // Так как, мне не хотелось сохранять ничего в файлы, используем МемориСтрим, не забывая оборачивать его юзингом.
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // Сериализуем в XML-формат.
                            var xmlFile = new XmlSerializer(temp.GetType());
                            // xmlFile.Serialize(Console.Out, temp);
                            xmlFile.Serialize(ms, temp);
                            // И вытаскиваем результат в строку.
                            ms.Seek(0, SeekOrigin.Begin);
                            StreamReader sr = new StreamReader(ms);
                            stringXML = sr.ReadToEnd();
                            sr.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка конвертации.");
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Для конвертации в XML переданны не полные данные");
            }
            // Возращаем получившуюся строку.
            return stringXML;
        }

    }
}
