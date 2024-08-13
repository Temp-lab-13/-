
using Autofac;
using Autofac.Extensions.DependencyInjection;
using WATask1.DadaBase;
using WATask1.DTO;
using WATask1.Repositori;

namespace WATask1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile)); // ���������� ������

            var config = new ConfigurationBuilder();// �����. ������������ ����� ��� ����������� ���������� ����� ������������.
            config.AddJsonFile("appsettings.json"); // ���������� ���� ������������. 
            var cfg = config.Build();               // � ���������, ��� �� �������� ������-������.

            // builder.Configuration.GetConnectionString("DataBase"); // ��� ���������� ���� �� ������� ����� ������������ appsettings.json, ����� ������������ ������ ���

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.RegisterType<UserRepo>().As<IUserRepo>(); // ��������� ����������, ��������� � ���������
                cb.Register(c => new AppDbContext(cfg.GetConnectionString("DataBase"))).InstancePerDependency(); // �������-�� ����������� ������-������ �� appsettings.json
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // ������� ��������� �������. ������ 38:58

            app.Run();
        }
    }
}
