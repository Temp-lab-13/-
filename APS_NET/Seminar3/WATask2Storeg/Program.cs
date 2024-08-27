
using Autofac.Extensions.DependencyInjection;
using Autofac;
using WATask2Storeg.Models;
using WATask2Storeg.Interface;
using WATask2Storeg.Mapper;
using WATask2Storeg.Query;
using WATask2Storeg.Mutatin;
using WATask2Storeg.Services;

namespace WATask2Storeg
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGraphQLServer()
               .AddQueryType<MySimpleQuery>() // ������� query
               .AddMutationType<MiSimpleMutation>(); // ������� mutation

            builder.Services.AddMemoryCache();                                      
            builder.Services.AddAutoMapper(typeof(MappingProfile)); // ������� ������
            builder.Services.AddTransient<IStoreService, StoreService>(); // ������� ��������� � ������
            builder.Services.AddEndpointsApiExplorer(); // ?
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(cd =>
            {
                cd.Register(c => new StoregContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency(); // ������� ��������
            });

            var app = builder.Build();

            app.MapGraphQL();
            app.Run();
        }
    }
}
