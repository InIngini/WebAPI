using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Errors;

namespace WebAPI.BLL.Errors
{
    /// <summary>
    /// Исключение, представляющее ошибку API.
    /// Позволяет передавать информацию об ошибке в виде объекта <see cref="ApiError"/>.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Получает объект <see cref="ApiError"/>, содержащий информацию об ошибке.
        /// </summary>
        public ApiError ApiError { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ApiException"/>.
        /// </summary>
        /// <param name="apiError">Объект <see cref="ApiError"/> с информацией об ошибке.</param>
        public ApiException(ApiError apiError) : base(apiError.Message)
        {
            ApiError = apiError;
        }
    }


}
