using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_3_PLINQ_and_asynchrony
{
    /*
     * Реализуйте метод ProcessMemoryStreamAsync таким образом, что бы он выводил на экран содержимое потока. 
    */
    internal class Task4
    {
        static async Task Exampl()
        {
            // Пример использования. Это надо в мейн запулять, что бы запускать.
            byte[] data = Encoding.UTF8.GetBytes("Hello, this is data for MemoryStream!");

            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                await ProcessMemoryStreamAsync(memoryStream);
            }
        }

        static async Task ProcessMemoryStreamAsync(MemoryStream memoryStream)
        {
            byte [] data =new byte[1024];
            var bytesRead = 0;
            StringBuilder sb = new StringBuilder();

            while ((bytesRead = await memoryStream.ReadAsync(data, 0, data.Length)) > 0)
            {
                sb.Append(Encoding.UTF8.GetString(data, 0, bytesRead));
            }
            await Console.Out.WriteLineAsync(sb.ToString());
        }

    }
}
