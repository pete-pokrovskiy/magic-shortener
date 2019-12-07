using Microsoft.Extensions.Configuration;
using System;

namespace MagicShortener.Common.Configuration
{
    /// <summary>
    /// Раширение-помощник для чтения конфигурации приложения
    /// </summary>
    public static class ConfigurationExtensions
    {
        public static string GetStringConfigParam(this IConfiguration config, string paramName)
        {
            var paramValue = config[paramName];
            if (string.IsNullOrEmpty(paramValue))
                throw new Exception($"Отсутствует или некорректно заполнен параметр {paramName}");

            return paramValue;
        }
    }
}
