using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Text;
using WebAPI.Token;

namespace WebAPI
{
    public class Program
    {
        //dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;
        //AttachDbFileName=C:\Users\vorob\source\repos\WebAPI\DB\bin\Debug\net8.0\DB\DB.mdf;Integrated Security=True;"
        //Microsoft.EntityFrameworkCore.SqlServer - писала в консольку для получения классов 

        public static string connectionString;
        public static void Main(string[] args)
        {
            // Получаем путь до файла бд
            var dataDirectory = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            var databasePath = Path.Combine(dataDirectory, "DB\\bin\\Debug\\net8.0\\DB\\DB.mdf");

            connectionString = $@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName={databasePath};Integrated Security=True;";
            
            
            // Создаем билдер
            var builder = WebApplication.CreateBuilder(args);

            // Регистрация контекста базы данных
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString));

            // Добавить контроллеры
            builder.Services.AddControllers();


            // Настройка аутентификации с использованием JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "your-issuer",
                        ValidAudience = "your-audience",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"))
                    };
                });
            // Регистрация ITokenService и ITokenValidator
            builder.Services.AddTransient<ITokenService, TokenService>();
            builder.Services.AddScoped<ITokenValidator, TokenValidator>();


            // Добавление авторизации
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            // Использование авторизации и аутентификации
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
       

    }
}
