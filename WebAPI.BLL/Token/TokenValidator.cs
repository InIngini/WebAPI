﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.DB.Entities;
using WebAPI.DB;
using Microsoft.Extensions.Configuration;


namespace WebAPI.BLL.Token
{
    /// <summary>
    /// Сервис для валидации JWT токенов.
    /// </summary>
    public class TokenValidator : ITokenValidator
    {
        private readonly IConfiguration _configuration;
        private readonly Context _dbContext;
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TokenValidator"/>.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        /// <param name="dbContext">Контекст базы данных.</param>
        public TokenValidator(IConfiguration configuration, Context dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }
        /// <summary>
        /// Валидирует указанный токен и возвращает идентификатор пользователя.
        /// </summary>
        /// <param name="token">JWT токен.</param>
        /// <returns>ID пользователя, если токен валиден; 0, если токен невалиден.</returns>
        public int ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };

                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                
                var userName = claimsPrincipal.FindFirstValue(ClaimTypes.Name);// Получаем логин из токена

                var user = _dbContext.Users.FirstOrDefault(u => u.Login == userName);// Получаем айдишник этого токена
                
                if (user != null)
                {
                    return user.IdUser; // Возвращаем айди пользователя
                }
                else
                {
                    return 0; // Ну или ошибка
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
