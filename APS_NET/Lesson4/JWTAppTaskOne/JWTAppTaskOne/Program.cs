
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
        static RSA GetPublicKey()   //  ������ ���� � public rsa �������, ������� rsa � ���������.
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


            builder.Services.AddTransient<IAuthenticationService, AuthenticationService>(); // ���������� ������ � ��� ����������.
            builder.Services.AddTransient<IUserService, UserService>();

            string path = builder.Configuration.GetConnectionString("db"); // ��� ��� ����� ���������� ���� ������ �� ����������������� ����� ��� ��������. ����� �� �������� �� ����� ������ �����������.
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(path));    // � ����� ��������� ��������, �������� ��� � ���� �������.

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // ���� �� ���������� ����-����� �� ����������������� �����.
                IssuerSigningKey = new RsaSecurityKey(GetPublicKey())   // ����� �� �� ��������� ����� ��� �� ����� ���������� rsa ������� �� ��������� ����� open rsa(�� �������, �� ����� ���������� ��� ���� ���������.)
            });


            var app = builder.Build();

  
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication(); // ������� ������ ���� ������ �����. ������� ��������������(��������, ������������).
            app.UseAuthorization(); // ����� �����������(��������� ������� ������������).


            app.MapControllers();

            app.Run();
        }
    }
}
