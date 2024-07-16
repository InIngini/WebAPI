namespace WebAPI.BLL.DTO
{
    public class ConnectionData
    {
        public int? IdConnection { get; set; }
        public int? IdBook { get; set; }
        public int IdCharacter1 { get; set; }
        public string? Name1 { get; set; }
        public int IdCharacter2 { get; set; }
        public string? Name2 { get; set; }
        public string TypeConnection { get; set; }
    }
}
