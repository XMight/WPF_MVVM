using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMightCommon
{
    public class Logger
    {
        private static bool _isInitialized = false;
        private static ILog _logger;

        public static void Initialize(Type pLoggerType, String pConfigFile)
        {
            if (!_isInitialized)
            {
                if (!String.IsNullOrEmpty(pConfigFile))
                {
                    XmlConfigurator.ConfigureAndWatch(new FileInfo(pConfigFile));
                }
                else
                {
                    XmlConfigurator.Configure();
                }

                _logger = log4net.LogManager.GetLogger(pLoggerType);
                _isInitialized = true;
            }
            else
            {
                throw new ApplicationException("Logger not initialized");
            }
        }

        public static XMightLogger GerLoggerForName(String pName, String pHeader = null)
        {
            ILog logger = log4net.LogManager.GetLogger(pName);
            log4net.Repository.Hierarchy.Logger l = (log4net.Repository.Hierarchy.Logger)logger.Logger;
            l.RemoveAllAppenders();

            DateFolderRollingFileAppender app1 = new DateFolderRollingFileAppender();
            app1.Name = pName + "Appender";
            app1.File = "logs\\" + pName;
            app1.PreserveLogFileNameExtension = true;
            app1.DatePattern = ".dd-MM-yyyy";
            app1.AppendToFile = false;
            app1.ImmediateFlush = true;
            app1.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date;
            app1.StaticLogFileName = false;

            log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout();
            layout.ConversionPattern = "%d{dd-MM-yyyy HH:mm:ss.fff}\t%timestamp\t%message%newline";

            if (!String.IsNullOrEmpty(pHeader))
            {
                layout.Header = pHeader + "\n";
            }

            layout.ActivateOptions();

            app1.Layout = layout;
            app1.ActivateOptions();

            l.AddAppender(app1);
            l.Additivity = false;
            l.Level = log4net.Core.Level.Debug;

            return new XMightLogger(logger);
        }

        private static void LogBase(LoggingLevel pLoggingLevel, String pMessage, Object pLoggingProperties, Exception pException)
        {
            if (_isInitialized && ShouldLog(_logger, pLoggingLevel))
            {
                switch (pLoggingLevel)
                {
                    case LoggingLevel.Debug: _logger.Debug(pMessage, pException); break;
                    case LoggingLevel.Info: _logger.Info(pMessage, pException); break;
                    case LoggingLevel.Warning: _logger.Warn(pMessage, pException); break;
                    case LoggingLevel.Error: _logger.Error(pMessage, pException); break;
                    case LoggingLevel.Fatal: _logger.Fatal(pMessage, pException); break;
                }
            }
        }

        private static bool ShouldLog(ILog pLog, LoggingLevel pLoggingLevel)
        {
            switch (pLoggingLevel)
            {
                case LoggingLevel.Debug: return pLog.IsDebugEnabled;
                case LoggingLevel.Info: return pLog.IsInfoEnabled;
                case LoggingLevel.Warning: return pLog.IsWarnEnabled;
                case LoggingLevel.Error: return pLog.IsErrorEnabled;
                case LoggingLevel.Fatal: return pLog.IsFatalEnabled;
                default: return false;
            }
        }
    }
}
