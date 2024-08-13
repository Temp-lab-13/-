
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;
using System.Text;
using UsersService.Abstract;
using UsersService.Services;
using UsersService.Models.Context;

namespace UsersService
{
    public class Program
    {
        static RSA GetPublicKey()
        {
            var file = File.ReadAllText("RSA/public_key.pem");
            var rsa = RSA.Create();
            rsa.ImportFromPem(file);
            return rsa;
        }

        public static WebApplication BuildWebApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Вставить токен",
                    Name = "LogIn",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "Token",

                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[] { }}

                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Настройка аутентификации JWT Bearer; 
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new RsaSecurityKey(GetPublicKey())
                    };
                });

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuild =>
            {
                containerBuild.RegisterType<UserRepo>().As<IUserRepo>();
                containerBuild.RegisterType<Methods>().As<IMethods>();
                containerBuild.Register(r => new AppDBContext(builder.Configuration.GetConnectionString("db"))).InstancePerDependency();
            });
            return builder.Build();

        }


        public static void Main(string[] args)
        {

            var app = BuildWebApp(args);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.Run();
        }
    }
    // openssl genpkey -algorithm RSA -out private_key.pem
    // openssl rsa -pubout -in private_key.pem -out public_key.pem
}
