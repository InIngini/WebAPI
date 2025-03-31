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
        //dotnet ef migrations add InitialCreate --project WebAPI.DB --startup-project WebAPI - ������ � ��������� ��� �������� ��������
        //�� �������� ������ ��� InitialCreate

        //dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;
        //AttachDbFileName=C:\Users\vorob\source\repos\WebAPI\DB\bin\Debug\net8.0\DB\DB.mdf;Integrated Security=True;"
        //Microsoft.EntityFrameworkCore.SqlServer - ������ � ��������� ��� ��������� ������� 

        public static void Main(string[] args)
        {
            // ������� ������
            var builder = WebApplication.CreateBuilder(args);

            // ������������ ��������
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();
            
            // ��������� �������� CORS
            app.UseCors("AllowAll");

            // �����: ������� ��������, ����� ������������� ������
            ApplyMigrations(app);     // ����� ����� ��� ��������
            InitializeData(app);      // ������������ �����

            // ��������� Middleware
            ConfigureMiddleware(app);

            // ������ ����������
            app.Run();
        }

        /// <summary>
        /// ������������� ������� ���������� � ������������ ��������� �������.
        /// </summary>
        /// <param name="services">������ �������� ��� ���������� ������������.</param>
        /// <param name="configuration">������������ ���������� ��� ������� � ����������.</param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // ����������� ��������� ���� ������
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // ����������� ���������� IContext
            services.AddScoped<IContext, Context>();

            // �������� �����������
            services.AddControllers();

            // ����������� ITokenService � ITokenValidator
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<ITokenValidator, TokenValidator>();

            // ��������������
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
                    Description = @"������� JWT ����� ����������� (����� ����� � ������� User/login).",
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

            // ����������� ��������
            RegisterApplicationServices(services);

            // ���������� �����������
            services.AddAuthorization();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin() // ��������� ������� � ������ ��������� (�������� �� ���-�� ����� ����������, � �� �� ������������...)
                               .AllowAnyMethod() // ��������� ����� HTTP ������ (GET, POST � �.�.)
                               .AllowAnyHeader(); // ��������� ����� ���������
                    });
            });
        }

        /// <summary>
        /// ������������� AutoMapper � ������������ ������� � ���������� ��������.
        /// </summary>
        /// <param name="services">������ �������� ��� ���������� �������� AutoMapper.</param>
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
        /// ������������ ������� ���������� � ���������� ������������.
        /// </summary>
        /// <param name="services">������ �������� ��� ���������� ������������.</param>
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

            // ����������� ������ ��� ���������� ������
            services.AddTransient<AddedData>();
            services.AddTransient<CreationRepository>();
            services.AddTransient<DeletionRepository>();
        }

        /// <summary>
        /// ��������� ��� ��������� �������� ���� ������
        /// </summary>
        private static void ApplyMigrations(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Context>();
                db.Database.Migrate(); // ������������� ��������� ��� ��������
            }
        }

        /// <summary>
        /// �������������� ������ � ���� ������ ��� ������ ����������, ���� ����� ������ ��� ���.
        /// </summary>
        /// <param name="app">��������� ���������� ��� �������� ������� ��������� ��������.</param>
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
                    // ����������� ������
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "������ ��� ������������� ������.");
                }
            }
        }


        /// <summary>
        /// ����������� ������������� ����������� ����������� (middleware) ��� ��������� �������� � ������.
        /// </summary>
        /// <param name="app">��������� ���������� ��� ��������� middleware.</param>
        public static void ConfigureMiddleware(WebApplication app)
        {
            // ��������� ��� ��������� ������
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

            // ������������� ����������� � ��������������
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
