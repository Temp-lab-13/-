
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace WebGateway
{
    public class Program
    {
        //  Прописывать настройки оцелота - это просто жесть.
        //  алсо. Для кажждого интерфейса, приходится прописывать конфиг индивидуально, даже если они все висят в одном сервесе.(продукт, католог, файлы)
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();   //  Поключение конфигурационного файла оцелота.
            builder.Services.AddOcelot(configuration);  // Добавление оцелота с указанной конфигурацией.
            builder.Services.AddSwaggerForOcelot(configuration);    //  Добавление свагера для оцелота с указанной всё там же конфигурацией.(можно ли рабивать конфигурацию на два файла?)

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            }).UseOcelot().Wait(); // Подключаем UI оцелота и указываем путь для рабочего файла.
            

            app.UseHttpsRedirection();
        
            app.Run();
        }
    }
}
