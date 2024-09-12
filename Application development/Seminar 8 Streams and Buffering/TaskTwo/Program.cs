
namespace TaskTwo
{
    internal class Program
    {
        /*
         * Задача №2:
         * Напишите утилиту рекрусивного поиска файлов в заданном каталоге и его подкоталогах.
         */
        static void Main(string[] args)
        {
            foreach (var item in args)
            {
                Console.WriteLine(item);
            }

            List<string> list = SearchIn(Path: args[0], name: args[1]);

            Console.WriteLine(String.Join("\n", list));
        }

        private static List<string> SearchIn(string Path, string name)
        {
            List<string> list = new List<string>();

            DirectoryInfo dir = new DirectoryInfo(Path);

            var directories = dir.GetDirectories();
            var fils = dir.GetFiles();

            foreach (var fil in fils)
            {
                if (fil.Name.Contains(name)) list.Add(fil.Name);
                if (fil.Extension.Contains(name)) list.Add(fil.Name);
            }

            foreach (var item in directories)
            {
                list.AddRange(SearchIn(item.FullName, name));
            }

            return list;
        }
    }
}
