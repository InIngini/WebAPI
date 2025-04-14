namespace WebAPI.Errors
{
    /// <summary>
    /// Представляет информацию об ошибке API.
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Получает или устанавливает сообщение об ошибке.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Получает или устанавливает код ошибки.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Получает или устанавливает дополнительные детали об ошибке.
        /// </summary>
        public IEnumerable<string> Details { get; set; }
    }

}
