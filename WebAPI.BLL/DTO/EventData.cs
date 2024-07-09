namespace WebAPI.BLL.DTO
{
    public class EventData
    {
        public int? IdBook { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }
        public int[]? IdCharacter { get; set; }
    }
}
