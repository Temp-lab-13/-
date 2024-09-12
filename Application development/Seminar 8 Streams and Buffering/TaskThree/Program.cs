namespace TaskThree
{
    internal class Program
    {
        /*
         * Напишите утилиту, читающую текстовый файл и выводящую строки содержащие искомое слово.
         * И напишите это слово в UpCase.
         * 
         * Для запуска использовать:  dotnet run
         */
        const string path = "Program.cs";
        const string wo = "return";

        static void Main(string[] args)
        {
            var text = ReadFrom(path);
            var resalt = Filtr(wo, text);
            Console.WriteLine(String.Join("\n", resalt));
        }

        static List<string> ReadFrom(string path)    // Читаем файл построчно, до самого его конца.
        {
            List<string> result = new List<string>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream) 
                {
                    result.Add(sr.ReadLine()!);
                }
            }
            return result;
        }

        static List<string> Filtr(string word, List<string> text)
        {
            return text.Where(x => x.ToLower().Contains(word.ToLower()))
                .Select(s => s.ToLower().Replace(word.ToLower(), word.ToUpper())).ToList();
        }
    }
}
