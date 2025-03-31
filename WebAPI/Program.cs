using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebAPI.Auth;
using WebAPI.BLL.Additional;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.Services;
using WebAPI.BLL.Token;
using WebAPI.DB;
using WebAPI.Errors;

namespace WebAPI
{
    public class Program
    {
        //dotnet ef migrations add InitialCreate --project WebAPI.DB --startup-project WebAPI - писала в консольку для создания миграции
        //не забывать менять имя InitialCreate

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

            // Важно: сначала миграции, потом инициализация данных
            ApplyMigrations(app);     // Новый метод для миграций
            InitializeData(app);      // Существующий метод

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

            // Регистрация интерфейса IContext
            services.AddScoped<IContext, Context>();

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

            // Swagger
            services.AddSwaggerGen(options =>
            {
                var basePath = AppContext.BaseDirectory;

                var xmlPath = "WebAPI.xml";
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Введите JWT токен авторизации (можно взять в запросе User/login).",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new List<string>()
                    }
                });
            });

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
            services.AddTransient<IAuthService, AuthService>();

            // Регистрация класса для добавления данных
            services.AddTransient<AddedData>();
            services.AddTransient<CreationRepository>();
            services.AddTransient<DeletionRepository>();
        }

        /// <summary>
        /// Применяет все ожидающие миграции базы данных
        /// </summary>
        private static void ApplyMigrations(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Context>();
                db.Database.Migrate(); // Автоматически применяет все миграции
            }
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

                try
                {
                    var addedData = services.GetRequiredService<AddedData>();
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

            app.UseMiddleware<BasicAuthMiddleware>();

            // Swagger
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
               });

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            // Использование авторизации и аутентификации
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
