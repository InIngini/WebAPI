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

namespace WebAPI
{
    public class Program
    {
        //dotnet ef migrations add InitialCreate --project C:\Users\vorob\source\repos\WebAPI\WebAPI.DB\WebAPI.DB.csproj - ������ � ��������� ��� �������� ��������

        //dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;
        //AttachDbFileName=C:\Users\vorob\source\repos\WebAPI\DB\bin\Debug\net8.0\DB\DB.mdf;Integrated Security=True;"
        //Microsoft.EntityFrameworkCore.SqlServer - ������ � ��������� ��� ��������� ������� 

        public static void Main(string[] args)
        {
            //using (var context = new Context()) { }
            
            // ������� ������
            var builder = WebApplication.CreateBuilder(args);

            // ����������� ��������� ���� ������
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // �������� �����������
            builder.Services.AddControllers();
   

            // ����������� ITokenService � ITokenValidator
            builder.Services.AddTransient<ITokenService, TokenService>();
            builder.Services.AddScoped<ITokenValidator, TokenValidator>();

            // ����� ������ ConfigureServices
            ConfigureServices(builder.Services);

            // ���������� �����������
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // ������������� ������
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
                    // ����������� ������
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "������ ��� ������������� ������.");
                }
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            // ������������� ����������� � ��������������
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }     

        public static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, TokenAuthenticationHandler>("TokenAuthentication", options => { });

            //mapper
            services.AddAutoMapper(config =>
            {
                //config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(Assembly.Load("WebAPI.BLL")));
                config.AddProfile(new AssemblyMappingProfile(Assembly.Load("WebAPI.DB")));
            });


            // ����������� ��������
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

            // ����������� ������ ��� ���������� ������
            services.AddTransient<AddedData>();
        }
    }
}
