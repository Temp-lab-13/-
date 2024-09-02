
using JWTAppTaskOne.Abstract;
using JWTAppTaskOne.AuthorizationModel.Abstract;
using JWTAppTaskOne.AuthorizationModel.Service;
using JWTAppTaskOne.Context;
using JWTAppTaskOne.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace JWTAppTaskOne
{
    public class Program
    {
        static RSA GetPublicKey()   //  Читаем файл с public rsa токеном, генерем rsa и возращаем.
        {
            var readFile = File.ReadAllText("RSA/public_key.pem");
            var rsa = RSA.Create();
            rsa.ImportFromPem(readFile);
            return rsa;
        } 

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Brearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "Token",
                    Scheme = "bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Brearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });


            builder.Services.AddTransient<IAuthenticationService, AuthenticationService>(); // Подключаем сервис с его интрфейсом.
            builder.Services.AddTransient<IUserService, UserService>();

            string path = builder.Configuration.GetConnectionString("db"); // Вот так можно подключить Базу данных из конфигурационного файла без афтофага. Сдесь мы получаем из файла строку подключения.
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(path));    // А здесь добавляем контекст, связывая его с этой строкой.

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Здес мы подключали ключ-токен из конфигурационного файла.
                IssuerSigningKey = new RsaSecurityKey(GetPublicKey())   // Здесь же мы поключаем токен уже из файла публичного rsa который мы сгенерили через open rsa(на линуксе, на винде подключать эту фичу геморойно.)
            });


            var app = builder.Build();

  
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication(); // Порядок должен быть именно такой. Сначало Аутентификация(проверям, пользователя).
            app.UseAuthorization(); // Потом Авторизация(проверяем доступы пользователя).


            app.MapControllers();

            app.Run();
        }
    }
}
