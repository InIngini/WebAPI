using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using System.IO;

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
            var dataDirectory = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            var databasePath = Path.Combine(dataDirectory, "DB\\bin\\Debug\\net8.0\\DB\\DB.mdf");

            connectionString = $@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName={databasePath};Integrated Security=True;";
            
            using (var connection = new SqlConnection(connectionString))
            {
                // Открыть соединение
                connection.Open();

                // Выполнить команды SQL
                // ...

                // Закрыть соединение
                connection.Close();
            }
            
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
