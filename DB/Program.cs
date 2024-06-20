namespace DB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //dotnet ef migrations add InitialCreate - писала в консольку для создания миграции

            //AddedData addedData = new AddedData(); //для добавления данных
            using (var context = new Context())
            { }
        }
    }
}
