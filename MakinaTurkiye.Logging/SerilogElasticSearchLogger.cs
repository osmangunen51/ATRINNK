using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Logging
{
    public class SerilogElasticSearchLogger :ILogger
    {

        private Serilog.Core.Logger _logger;
        public SerilogElasticSearchLogger()
        {
            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "Makinaturkiye.Green")
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    TemplateName = "serilog-events-template",
                    AutoRegisterTemplate = true,
                    IndexFormat = "makinaturkiye-log-{0:yyyy.MM.dd}" 
                });
            _logger = loggerConfig.CreateLogger();
        }


        public ILogger CreateChildLogger(string loggerName)
        {
            throw new NotImplementedException();
        }

        public void Critical(string message)
        {
            if (!IsCriticalEnabled)
                return;
        }

        public void Critical(Func<string> messageFactory)
        {
            if (!IsCriticalEnabled)
                return;
        }

        public void Critical(string message, Exception exception)
        {
            if (!IsCriticalEnabled)
                return;
        }

        public void CriticalFormat(string format, params string[] args)
        {
            if (!IsCriticalEnabled)
                return;
        }

        public void CriticalFormat(Exception exception, string format, params object[] args)
        {
            if (!IsCriticalEnabled)
                return;
        }

        public void CriticalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsCriticalEnabled)
                return;
        }

        public void CriticalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsCriticalEnabled)
                return;
        }

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Debug(Func<string> messageFactory)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(Exception exception, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }


        public void Error(string message)
        {
            if (!IsErrorEnabled)
                return;

            _logger.Error(message);
        }

        public void Error(Func<string> messageFactory)
        {
            if (!IsErrorEnabled)
                return;
        }

        public void Error(string message, Exception exception)
        {
            if (!IsErrorEnabled)
                return;

            _logger.Error(exception, message);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
        }

        public void ErrorFormat(Exception exception, string format, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
        }

        public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
        }

        public void Fatal(string message)
        {
            if (!IsFatalEnabled)
                return;

            _logger.Fatal(message);
        }

        public void Fatal(Func<string> messageFactory)
        {
            if (!IsFatalEnabled)
                return;
        }

        public void Fatal(string message, Exception exception)
        {
            if (!IsFatalEnabled)
                return;

            _logger.Fatal(exception, message);
        }

        public void FatalFormat(string format, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
        }

        public void FatalFormat(Exception exception, string format, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
        }

        public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(Func<string> messageFactory)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void InfoFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void InfoFormat(Exception exception, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message)
        {
            throw new NotImplementedException();
        }

        public void Trace(Func<string> messageFactory)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void TraceFormat(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void TraceFormat(Exception exception, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void TraceFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(Func<string> messageFactory)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(string format, params string[] args)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(Exception exception, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            throw new NotImplementedException();
        }


        private bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return false;
                default:
                    return true;
            }
        }

        public bool IsTraceEnabled => IsEnabled(LogLevel.Trace);

        public bool IsDebugEnabled => IsEnabled(LogLevel.Debug);

        public bool IsErrorEnabled => IsEnabled(LogLevel.Error);

        public bool IsFatalEnabled => IsEnabled(LogLevel.Fatal);

        public bool IsInfoEnabled => IsEnabled(LogLevel.Info);

        public bool IsWarnEnabled => IsEnabled(LogLevel.Warn);

        public bool IsCriticalEnabled => IsEnabled(LogLevel.Critical);
    }
}
