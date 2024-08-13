using HomeWork.Model;
using HomeWork.Service;

namespace HomeWork;

// Важно. Для использования dotnet ef надо установить через консоль слудещее: dotnet tool install --global dotnet-ef
internal class Program
{
 /*   
    Мы планируем расширить функциональность нашего чат-приложения, добавив поддержку
    работы с базой данных.
    Создайте модель базы данных для чата, используя подход CodeFirst.Начните с создания двух
    таблиц: Messages (Сообщения) и Users (Пользователи). Убедитесь, что модель учитывает, что
    каждое сообщение имеет автора, адресата и статус получения сообщения адресатом.
 */
    static async Task Main(string[] args)
    {
        await new UDPServer().StartServer();

    }
}
