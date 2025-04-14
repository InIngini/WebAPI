using System.ComponentModel.DataAnnotations;

namespace WebAPI.DB.CommonAppModel
{
    /// <summary>
    /// Данные логина и пароля для доступа к сваггеру
    /// </summary>
    public class SwaggerLogin
    {
        [Key]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
