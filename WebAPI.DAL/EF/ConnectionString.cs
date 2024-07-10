using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DAL.EF
{
    public class ConnectionString
    {
        public static string connectionString;
        public static void ConnectString()
        {
            // Получаем путь до файла бд
            var dataDirectory = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            var databasePath = Path.Combine(dataDirectory, "DB\\bin\\Debug\\net8.0\\DB\\DB.mdf");

            connectionString = $@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName={databasePath};Integrated Security=True;";

        }
    }
}