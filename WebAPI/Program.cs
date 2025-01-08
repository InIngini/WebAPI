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
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using WebAPI.BLL.Mappings;
using AutoMapper;
using System.Reflection;
using WebAPI.Errors;

namespace WebAPI
{
    public class Program
    {
        //dotnet ef migrations add InitialCreate --project C:\Users\vorob\source\repos\WebAPI\WebAPI.DB\WebAPI.DB.csproj - писала в консольку для создания миграции

        //dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;
        //AttachDbFileName=C:\Users\vorob\source\repos\WebAPI\DB\bin\Debug\net8.0\DB\DB.mdf;Integrated Security=True;"
        //Microsoft.EntityFrameworkCore.SqlServer - писала в консольку для получения классов 

        public static void Main(string[] args)
        {
            // Создаем билдер
            var builder = WebApplication.CreateBuilder(args);

            // Конфигурация сервисов
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();
            
            // Применяет политику CORS
            app.UseCors("AllowAll");

            // Инициализация данных
            InitializeData(app);

            // Настройка Middleware
            ConfigureMiddleware(app);

            // Запуск приложения
            app.Run();
        }

        /// <summary>
        /// Конфигурирует сервисы приложения и регистрирует зависимые сервисы.
        /// </summary>
        /// <param name="services">Сборка сервисов для добавления зависимостей.</param>
        /// <param name="configuration">Конфигурация приложения для доступа к настройкам.</param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Регистрация контекста базы данных
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Добавить контроллеры
            services.AddControllers();

            // Регистрация ITokenService и ITokenValidator
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<ITokenValidator, TokenValidator>();

            // Аутентификация
            services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, TokenAuthenticationHandler>("TokenAuthentication", options => { });

            // AutoMapper
            ConfigureAutoMapper(services);

            // Регистрация сервисов
            RegisterApplicationServices(services);

            // Добавление авторизации
            services.AddAuthorization();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin() // Разрешает запросы с любого источника (поменять на что-то более конкретное, а то не безопасновое...)
                               .AllowAnyMethod() // Разрешает любые HTTP методы (GET, POST и т.д.)
                               .AllowAnyHeader(); // Разрешает любые заголовки
                    });
            });
        }

        /// <summary>
        /// Конфигурирует AutoMapper и регистрирует профили в контейнере сервисов.
        /// </summary>
        /// <param name="services">Сборка сервисов для добавления профилей AutoMapper.</param>
        public static void ConfigureAutoMapper(IServiceCollection services)
        {
            var assembly = Assembly.Load("WebAPI.BLL");
            var profiles = assembly.GetExportedTypes()
                .Where(type => typeof(Profile).IsAssignableFrom(type)
                               && !type.IsAbstract
                               && type.Namespace == "WebAPI.BLL.Mappings.ProfileForMapping");

            services.AddAutoMapper(cfg => cfg.AddMaps(profiles));
        }

        /// <summary>
        /// Регистрирует сервисы приложения в контейнере зависимостей.
        /// </summary>
        /// <param name="services">Сборка сервисов для добавления зависимостей.</param>
        public static void RegisterApplicationServices(IServiceCollection services)
        {
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

            // Регистрация класса для добавления данных
            services.AddTransient<AddedData>();
        }

        /// <summary>
        /// Инициализирует данные в базе данных при старте приложения, если таких данных еще нет.
        /// </summary>
        /// <param name="app">Экземпляр приложения для создания области видимости сервисов.</param>
        public static void InitializeData(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var addedData = services.GetRequiredService<AddedData>();

                try
                {
                    addedData.Initialize();
                }
                catch (Exception ex)
                {
                    // Логирование ошибок
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ошибка при инициализации данных.");
                }
            }
        }

        /// <summary>
        /// Настраивает промежуточное программное обеспечение (middleware) для обработки запросов и ошибок.
        /// </summary>
        /// <param name="app">Экземпляр приложения для настройки middleware.</param>
        public static void ConfigureMiddleware(WebApplication app)
        {
            // Миддлвейр для обработки ошибок
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            // Использование авторизации и аутентификации
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
