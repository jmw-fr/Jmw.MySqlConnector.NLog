// <copyright file="NLogLogger.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.MySqlConnector.NLog
{
    using System;
    using System.Globalization;
    using global::MySqlConnector.Logging;
    using global::NLog;

    /// <summary>
    /// NLog logger for MySQLConnector.
    /// </summary>
    internal class NLogLogger : IMySqlConnectorLogger
    {
        private Logger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NLogLogger"/> class.
        /// </summary>
        /// <param name="logger">Logger NLog</param>
        public NLogLogger(Logger logger) => this.logger = logger;

        /// <inheritdoc />
        public bool IsEnabled(MySqlConnectorLogLevel level) => this.logger.IsEnabled(MapLevel(level));

        /// <inheritdoc />
        public void Log(MySqlConnectorLogLevel level, string message, object[] args = null, Exception exception = null)
        {
            string formattedMessage;

            if (args == null || args.Length == 0)
            {
                formattedMessage = message;
            }
            else
            {
                formattedMessage = string.Format(CultureInfo.InvariantCulture, message, args);
            }

            if (exception == null)
            {
                this.logger.Log(MapLevel(level), formattedMessage);
            }
            else
            {
                this.logger.Log(MapLevel(level), exception, formattedMessage);
            }
        }

        private static LogLevel MapLevel(MySqlConnectorLogLevel level)
        {
            switch (level)
            {
                case MySqlConnectorLogLevel.Trace:
                    return LogLevel.Trace;
                case MySqlConnectorLogLevel.Debug:
                    return LogLevel.Debug;
                case MySqlConnectorLogLevel.Info:
                    return LogLevel.Info;
                case MySqlConnectorLogLevel.Warn:
                    return LogLevel.Warn;
                case MySqlConnectorLogLevel.Error:
                    return LogLevel.Error;
                case MySqlConnectorLogLevel.Fatal:
                    return LogLevel.Fatal;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, "Invalid value for 'level'.");
            }
        }
    }

}
