using System.Text.Json;
using System.Xml.Serialization;
using HomeWork.Converts;
using HomeWork.MyClass;
using HomeWork.Serializations;

namespace HomeWork
{
    internal class Program
    {
        /*
         * Итоговое атестационное задание:
         * 
         * Напишите приложение конвертирующее произвольный JSON в XML. 
         */
        static void Main(string[] args)
        {
            // Создаём объект с которым будем работать.
            MechPark mechPark = CreateMechClass();

            // Создаём JSON-строку на основе полученого объекта, которую надо конвертировать в XML.
            SerialazJson serialazJson = new SerialazJson();
            var jsonString = serialazJson.SerialazToJson(mechPark);

            // Вывоим соержимое в консоль, для проверки.
            Console.WriteLine("JSON-File: \n");
            Console.WriteLine(jsonString);
            Console.WriteLine("\n");

            // Конвертируем JSON-строку в XML-формат, передавая саму строку и тип класса, которым она была.
            ConvertToXML convertToXML = new ConvertToXML();
            var xmlString = convertToXML.ConvertXML(jsonString, mechPark.GetType());

            // Вывоим соержимое в консоль, для проверки.
            Console.WriteLine("XML-File: \n");
            Console.WriteLine(xmlString);
            Console.WriteLine("\n");
        }


        // Создание и наполнение тестового класса.
        private static MechPark CreateMechClass()
        {
            MechPark Tortuga = new MechPark() { Location = "X51", UnitsOfEquipment = 47 };
            Mech c26 = new Mech()
            {
                Name = "V121",
                Type = "Ascolot",
                ProductionDate = new DateTime(2035, 2, 20),
                Size = 4.2,
                MaxSpeed = 70
            };

            Mech G206 = new Mech()
            {
                Name = "Ser12",
                Type = "Glader",
                ProductionDate = new DateTime(2029, 5, 1),
                Size = 2,
                MaxSpeed = 120
            };

            Mech HEVEC = new Mech()
            {
                Name = "010100",
                Type = "X",
                ProductionDate = new DateTime(2037, 10, 31),
                Size = 5.7,
                MaxSpeed = 55
            };
            Tortuga.Meches.Add(c26);
            Tortuga.Meches.Add(G206);
            Tortuga.Meches.Add(HEVEC);

            return Tortuga;
        }
    }
}
