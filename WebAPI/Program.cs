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
using WebAPI.DAL.EF;
using WebAPI.DAL.Interfaces;
using WebAPI.DAL.Repositories;

namespace WebAPI
{
    public class Program
    {
        //dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;
        //AttachDbFileName=C:\Users\vorob\source\repos\WebAPI\DB\bin\Debug\net8.0\DB\DB.mdf;Integrated Security=True;"
        //Microsoft.EntityFrameworkCore.SqlServer - ������ � ��������� ��� ��������� ������� 

        public static void Main(string[] args)
        {
            
            ConnectionString.ConnectString();
            
            // ������� ������
            var builder = WebApplication.CreateBuilder(args);

            // ����������� ��������� ���� ������
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(ConnectionString.connectionString));

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
            // ����������� ��������
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
