using FluentValidation;

namespace WebAPI.ConfigurationValidation.Validators
{
    public class ConfigurationValidator : AbstractValidator<IConfiguration>
    {
        public ConfigurationValidator()
        {
            RuleFor(config => config)
                .Must(config => ValidateConfigurationValues(config) == null)
                .WithMessage(config => ValidateConfigurationValues(config));
        }

        private string ValidateConfigurationValues(IConfiguration configuration)
        {
            return ContainsForbiddenValue(configuration, "changeme");
        }

        private string ContainsForbiddenValue(IConfiguration configuration, string forbiddenValue)
        {
            foreach (var section in configuration.GetChildren())
            {
                if (section.Value != null && section.Value.Contains(forbiddenValue, StringComparison.OrdinalIgnoreCase))
                {
                    return $"Значение '{section.Path}' содержит запрещенное значение 'changeme'.";
                }

                // Рекурсивная проверка всех дочерних секций
                var childError = ContainsForbiddenValue(section, forbiddenValue);
                if (childError != null)
                {
                    return childError; // Возвращаем ошибку, если найдена в дочерней секции
                }
            }

            return null; // Ничего не найдено
        }
    }
}
