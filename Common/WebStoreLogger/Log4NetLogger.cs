using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Xml;

namespace WebStoreLogger
{
    {
        private readonly ILog _Log;

        public Log4NetLogger(string Category, XmlElement Configuration)
        {
            var logger_repository = LogManager
               .CreateRepository(
                    Assembly.GetEntryAssembly(),
                    typeof(log4net.Repository.Hierarchy.Hierarchy));

            _Log = LogManager.GetLogger(logger_repository.Name, Category);

            log4net.Config.XmlConfigurator.Configure(logger_repository, Configuration);
    }
    {
                default: throw new ArgumentOutOfRangeException(nameof(Level), Level, null);
                case LogLevel.None: break;

                case LogLevel.Trace:
                case LogLevel.Debug:
                    _Log.Debug(log_message);
                    break;

                case LogLevel.Information:
                    _Log.Info(log_message);
                    break;

                case LogLevel.Warning:
                    _Log.Warn(log_message);
                    break;

                case LogLevel.Error:
                    _Log.Error(log_message, Error);
                    break;

                case LogLevel.Critical:
                    _Log.Fatal(log_message, Error);
                    break;
            }
    }
    public class Log4NetLogger
    {
    }

}
