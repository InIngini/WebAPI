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
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.Services;
using WebAPI.BLL.Token;
using WebAPI.DB;
using WebAPI.DAL.Interfaces;
using WebAPI.DAL.Repositories;
using System.Diagnostics;

namespace WebAPI
{
    public class Program
    {
        //dotnet ef migrations add InitialCreate - писала в консольку для создания миграции

        //dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;
        //AttachDbFileName=C:\Users\vorob\source\repos\WebAPI\DB\bin\Debug\net8.0\DB\DB.mdf;Integrated Security=True;"
        //Microsoft.EntityFrameworkCore.SqlServer - писала в консольку для получения классов 

        public static void Main(string[] args)
        {
            
            //ConnectionString.ConnectString();
            
            // Создаем билдер
            var builder = WebApplication.CreateBuilder(args);

            // Регистрация контекста базы данных
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\VOROB\SOURCE\REPOS\WEBAPI\WEBAPI.DB\BIN\DEBUG\NET8.0\DB\DB.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

            // Добавить контроллеры
            builder.Services.AddControllers();


            

            // Регистрация ITokenService и ITokenValidator
            builder.Services.AddTransient<ITokenService, TokenService>();
            builder.Services.AddScoped<ITokenValidator, TokenValidator>();

            // Вызов метода ConfigureServices
            ConfigureServices(builder.Services);

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

        public static void ConfigureServices(IServiceCollection services)
        {
            // Регистрация сервисов
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            services.AddTransient<IAddedAttributeService, AddedAttributeService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<IConnectionService, ConnectionService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IGalleryService, GalleryService>();
            services.AddTransient<IPictureService, PictureService>();
            services.AddTransient<ISchemeService, SchemeService>();
            services.AddTransient<ITimelineService, TimelineService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
