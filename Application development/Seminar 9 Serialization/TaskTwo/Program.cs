using System.Text.Json;

namespace TaskTwo
{
    internal class Program
    {
        /*
         * С сайта о погоде была получена информация о текущей и прошлой (за три дня) погоде, виде JSON.
         * Напишите класс способный хранить представленную информацию.
         * Десиарефлизуйте json-строку.
         */
        static void Main(string[] args)
        {
            string jsonString = "{\"Current\":{\"Time\":\"2023-06-18T20:35:06.722127+04:00\",\"Temperature\":29,\"Weathercode\":1,\"Windspeed\":2.1,\"Winddirection\":1},\"History\":[{\"Time\":\"2023-06-17T20:35:06.77707+04:00\",\"Temperature\":29,\"Weathercode\":2,\"Windspeed\":2.4,\"Winddirection\":1}, {\"Time\":\"2023-06-16T20:35:06.777081+04:00\",\"Temperature\":22,\"Weathercode\":2,\"Windspeed\":2.4,\"Winddirection\":1}, {\"Time\":\"2023-06-16T20:35:06.777082+04:00\",\"Temperature\":21,\"Weathercode\":4,\"Windspeed\":2.2,\"Winddirection\":1}]}";

            var weatherInfo = JsonSerializer.Deserialize<WeatherInfo>(jsonString);
            Console.ReadLine();
        }
    }
    public class Weather
    {
        public DateTime Time { get; set; }
        public double Temperatur { get; set; }
        public int Weathercode { get; set; }
        public double Windspeed { get; set; }
        public int Winddirection { get; set; }
    }

    public class WeatherInfo
    {
        public Weather Current { get; set; }
        public List<Weather> History { get; set; }
    }
}
