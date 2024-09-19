using FluentValidation;

using WebAPI.ConfigurationValidation.Validators;

namespace WebAPI.ConfigurationValidation
{
    public static class ValidateConfigurationExtensions
    {
        public static IServiceCollection AddConfigurationValidation(this IServiceCollection services, IConfiguration configuration)
        {
            // Регистрируем валидатор
            services.AddScoped<IValidator<IConfiguration>, ConfigurationValidator>();

            // Валидация во время старта приложения
            ValidateConfiguration(services, configuration);

            return services;
        }

        private static void ValidateConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var validator = serviceProvider.GetRequiredService<IValidator<IConfiguration>>();
                var result = validator.Validate(configuration);
                if (!result.IsValid)
                {
                    // Сформируем сообщение об ошибках
                    var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage));
                    throw new InvalidOperationException($"Валидация конфигурации не прошла:{Environment.NewLine}{errors}");
                }
            }
        }
    }
}
