using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;

namespace WebStore.Logger
{
    public static class Log4NetLoggerFactoryExtensions
    {
        private static string CheckFilePath(string FilePath)
        {
            if (FilePath is not { Length: > 0 })
                throw new ArgumentException("Не задан путь к файлу", nameof(FilePath));

            if (Path.IsPathRooted(FilePath)) return FilePath;

            var assembly = Assembly.GetEntryAssembly();//точка входа
            var dir = Path.GetDirectoryName(assembly!.Location);
            return Path.Combine(dir!, FilePath);
        }

        public static ILoggerFactory AddLog4Net(this ILoggerFactory Factory, string ConfigurationFile = "log4net.config")
        {
            Factory.AddProvider(new Log4NetLoggerProvider(CheckFilePath(ConfigurationFile)));
            return Factory;
        }
    }

}
