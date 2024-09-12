
using System.Xml;

namespace TaskOne
{
    internal class Program
    {
        /*
         * Задача №1:
         * Напишите консольную утилиту для копирования файлов
         * Пример запуска:
         *  utility.exe file.txt file2.txt
         *  
         * Команда для запуска моего решения:  dotnet run File.txt TestOne.txt
         */
        static void Main(string[] args)
        {
            foreach (var item in args)
            {
                Console.WriteLine(item);
            }

            string file = ReadFrom(args[0]);
            WriteTo(file, args[1]);
        }

        private static void WriteTo(string file, string path)
        {

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using StreamWriter writer = new StreamWriter(fs);
                writer.Write(file);
            };
        }

        private static string ReadFrom(string path)
        {
            using StreamReader sr = new StreamReader(path);
            return sr.ReadToEnd();
        }
    }
}
