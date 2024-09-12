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
            SearchIn(path: args[0], extension: args[1], text: args[2]);
        }


         // Этим методом мы теперь уже ничего не возращаем, последовательно отдавая результаты поиска на печать специальному методу.
        private static void SearchIn(string path, string extension, string text) // Метод поиска файлов в указанной директории и её подкаталогов.
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            var directories = dir.GetDirectories();
            var fils = dir.GetFiles();

            foreach (var fil in fils)
            {
                if (fil.Extension.Contains(extension))
                {
                    // Если нашли файл нужного расширения, то отправляем его в метод чтения содерижмого фала.
                    List<string> list = ReadFrom(path, fil.Name, text);

                    if (list.Count > 0) // Если лист пустой, т.е. в найденном файле с нашим расширением нет ключевого слова, то мы этот файл пропускаем и не печатаем.
                    {
                        // Вызываем метод рапечатывающий промежутлчные результаты.
                        // Данные выводяться последовательно, по мере их нахождения.
                        Print(path, fil.Name, list);
                    }
                }
            }

            foreach (var item in directories) 
            {
                SearchIn(item.FullName, extension, text);
            }
        }


        static List<string> ReadFrom(string path, string name, string text)    // Метод четния файла.
        {
            List<string> result = new List<string>();
            string fullpath = Path.Combine(path, name);

            using (StreamReader sr = new StreamReader(fullpath))
            {
                while (!sr.EndOfStream)
                {
                    result.Add(sr.ReadLine()!);
                }
            }
            // Прочитав файл, пропускаем соирфимое через фильт, который возвращает лишь нужные нам строки.
            return Filtr(text, result);
        }

        static List<string> Filtr(string word, List<string> text)   // Метод поиска ключевого слова в тексте и вывода строки с этим словом.
        {
            return text.Where(x => x.ToLower().Contains(word.ToLower()))
                .Select(s => s.ToLower().Replace(word.ToLower(), word.ToUpper())).ToList();
        }

        static void Print(string directory, string file,  List<string> text)    // Метод вывода в консоль результатов поиска.
        {
            Console.WriteLine($"Path:   {directory}");
            Console.WriteLine($"File:   {file}");
            Console.WriteLine("Text:    ");
            Console.WriteLine(String.Join("\n", text));
            Console.WriteLine();
        }

    }
}
