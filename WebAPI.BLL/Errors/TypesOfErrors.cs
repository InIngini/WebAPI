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
        public static ApiError NotValidModel(ModelStateDictionary modelState)
        {
            return new ApiError
            {
                Message = "Модель не валидна.",
                Code = "ValidationError",
                Details = modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
            };
        }
        /// <summary>
        /// Для ошибки, когда модель не валидна. (для сервисов)
        /// </summary>
        /// <returns>Сообщение об ошибке.</returns>
        public static string NotValidModel()
        {
            return "Модель не валидна.";
        }

        /// <summary>
        /// Для ошибки, когда объект не найден.
        /// </summary>
        /// <param name="objName">Название объекта, который не найден.</param>
        /// <param name="genderOrNumber">Род или число, чтобы склонять сообщение (0 - ж род, 1 - м род, 2 - ср род, 3 - мн число).</param>
        /// <returns>Строка с пояснением ошибки.</returns>
        public static string NotFoundById(string objName,int genderOrNumber)
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
        /// Для ошибки, когда объект не принадлежит пользователю.
        /// </summary>
        /// <param name="objName">Название объекта, который не найден.</param>
        /// <param name="genderOrNumber">Род или число, чтобы склонять сообщение (0 - ж род, 1 - м род, 2 - ср род, 3 - мн число).</param>
        /// <returns>Строка с пояснением ошибки.</returns>
        public static string NotYour(string objName,int genderOrNumber)
        {
            switch (genderOrNumber)
            {
                case 0: return $"Эта {objName} не ваша."; //ж род, ед число
                case 1: return $"Этот {objName} не ваш.";  //м род, ед число 
                case 2: return $"Это {objName} не ваше."; //ср род, ед число
                case 3: return $"Эти {objName} не ваши."; //мн число
                default: return $"Объект не ваш."; 
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
        /// <param name="ex">Ошибка.</param>
        /// <returns>Объект с информацией об ошибке.</returns>
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

        /// <summary>
        /// Для ошибки, когда что-то пошло не так.
        /// </summary>
        /// <param name="message">Сообщение об ошибке в качестве детали.</param>
        /// <returns>Строка с пояснением ошибки.</returns>
        public static ApiError SomethingWentWrong(string message)
        {
            return new ApiError
            {
                Message = "Что-то пошло не так...",
                Code = "InternalServerError",
                Details = new List<string>
                {
                    message
                }
            };
        }
    }
}
