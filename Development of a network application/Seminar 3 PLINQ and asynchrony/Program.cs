namespace Seminar_3_PLINQ_and_asynchrony
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Задача №1:
            /*
            var taska1 = Task1.Taska1();
            var taska2 = Task1.Taska2();
            int num1 = await taska1; // Оба способа позволяют получить интовый результат.
            int num2 = taska2.Result; //

            Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
            */

            // Задача №2:
            Task2.Taska2();
        }
    }
}
