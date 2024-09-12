using System.IO;
using System.Xml.Linq;

namespace HomeWork
{
    internal class Program
    {
        /*
         * Домашнее заданяие:
         * Объедините две предыдущих работы (практические работы 2 и 3): 
         * поиск файла и поиск текста в файле; 
         * написав утилиту которая ищет файлы определенного расширения с указанным текстом. Рекурсивно. 
         * 
         * Пример вызова утилиты: utility.exe txt текст
         */

        static void Main(string[] args)
        {
            // Принимаем:
            // args[0] - путь, где ищем;
            // args[1] - расширение, типы файлов, которые ищем;
            // args[2] - ключевое слово, которое ищем в тексте найденных файлов. 
            Search search = new Search();
            try
            {
                search.SearchIn(path: args[0], extension: args[1], text: args[2]);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}
