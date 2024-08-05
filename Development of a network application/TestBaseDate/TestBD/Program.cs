using Npgsql;

namespace TestBD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Тестируем AOD и работу с постгрес. 
            TestADOPostgress test1 = new TestADOPostgress();
            test1.AODPostgreSql(); // Работает. Соединение с базой данных есть. 

            
        }
    }
}
