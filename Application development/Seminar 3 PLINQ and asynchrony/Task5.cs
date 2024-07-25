using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_3_PLINQ_and_asynchrony
{
    /*
     * Напишите пример асинхронной работы с массивом, 
     * где сначала будет производится изменения элементов массива (например умножение на 2), 
     * а затем суммирование и возвращение результата. 
     * Реализуйте параллельное выполнение всех операций (включая изменение элементов массива). 
    */
    internal class Task5
    {
        static async Task Exampl() // Шаблон, то же для мейна
        {
            // Пример массива данных (может быть заменен на реальные данные)
            int[] data = { 1, 2, 3, 4, 5 };
            try
            {
                // Асинхронная обработка массива
                Task<int[]> processedDataTask = ProcessArrayAsync(data);

                // Асинхронный вывод результатов на консоль
                Console.WriteLine("\nProcessed Data:");
                int[] processedData = await processedDataTask;
                foreach (var item in processedData)
                {
                    Console.Write($"{item} ");
                }

                // Асинхронная операция после обработки массива с использованием ContinueWith
                var sumTask = processedDataTask.ContinueWith(t => ProcessSumAsync(t.Result));
                int sum = await await sumTask;
                Console.WriteLine($"\nSum of Processed Data: {sum}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

        }

        static async Task<int> ProcessElementAsync(int number)
        {
            await Task.Delay(1000);
            return number * 2;
        }

        static async Task<int[]> ProcessArrayAsync(int[] array)
        {
           return await Task.WhenAll(Array.ConvertAll(array, async (intem) => await ProcessElementAsync(intem)));
        }

        static async Task<int> ProcessSumAsync(int[] array)
        {
            await Task.Delay(1000);
            return array.Sum();
        }
    }
}
