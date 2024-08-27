using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Errors
{
    /// <summary>
    /// Статический класс с типами ошибок
    /// </summary>
    public static class TypesOfErrors
    {
        /// <summary>
        /// Для ошибки, когда модель не валидна.
        /// </summary>
        /// <param name="modelState">Модель.</param>
        /// <returns>Тип <see cref="ApiError"/> с сообщением об ошибке.</returns>
        public static ApiError NoValidModel(ModelStateDictionary modelState)
        {
            return new ApiError
            {
                Message = "Модель не валидна.",
                Code = "ValidationError",
                Details = modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
            };
        }
        /// <summary>
        /// Для ошибки, когда модель не валидна.
        /// </summary>
        /// <returns>Сообщение об ошибке.</returns>
        public static string NoValidModel()
        {
            return "Модель не валидна.";
        }

        /// <summary>
        /// Для ошибки, когда объект не найден.
        /// </summary>
        /// <param name="objName">Название объекта, который не найден.</param>
        /// <param name="genderOrNumber">Род или число, чтобы склонять сообщение (0 - ж род, 1 - м род, 2 - ср род, 3 - мн число).</param>
        /// <returns>Строка с пояснением ошибки.</returns>
        public static string NoFoundById(string objName,int genderOrNumber)
        {
            switch (genderOrNumber)
            {
                case 0: return $"{objName} не найдена."; //ж род, ед число
                case 1: return $"{objName} не найден.";  //м род, ед число 
                case 2: return $"{objName} не найдено."; //ср род, ед число
                case 3: return $"{objName} не найдены."; //мн число
                default: return $"Объект не найден."; 
            }
            
        }

        /// <summary>
        /// Для ошибки, когда пользователь не найден.
        /// </summary>
        /// <param name="userName">Логин пользователя, который не найден.</param>
        /// <returns>Строка с пояснением ошибки.</returns>
        public static string UserNotFound(string userName)
        {
            return $"Пользователь с логином '{userName}' не найден.";
        }
        /// <summary>
        /// Для ошибки, когда пользователь уже существует.
        /// </summary>
        /// <param name="userName">Логин пользователя, который существует.</param>
        /// <returns>Строка с пояснением ошибки.</returns>
        public static string UserFound(string userName)
        {
            return $"Пользователь с логином '{userName}' уже существует.";
        }

        /// <summary>
        /// Для ошибки, когда токен не прошел валидацию.
        /// </summary>
        /// <returns>Строка с пояснением ошибки.</returns>
        public static ApiError TokenNotValid(Exception ex)
        {
            //return $"Токен не валиден.";
            return new ApiError
            {
                Message = "Токен не валиден.",
                Code = "ValidationError",
                Details = new List<string>
                {
                    ex.Message, // Сообщение об ошибке
                    ex.StackTrace // Стек вызовов
                }
            };
        }
    }
}
