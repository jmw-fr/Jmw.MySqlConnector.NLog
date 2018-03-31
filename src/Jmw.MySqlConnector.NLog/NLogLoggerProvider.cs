// <copyright file="Log4netLoggerProvider.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.MySqlConnector.NLog
{
    using global::MySqlConnector.Logging;
    using global::NLog;

    /// <summary>
    /// NLog provider for MySqlConnector. Created from log4net provider https://github.com/mysql-net/MySqlConnector/tree/master/src/MySqlConnector.Logging.log4net.
    /// </summary>
    public sealed class NLogLoggerProvider : IMySqlConnectorLoggerProvider
    {
        /// <summary>
        /// Creates a new logger
        /// </summary>
        /// <param name="name">Logger Name</param>
        /// <returns>Created logger</returns>
        public IMySqlConnectorLogger CreateLogger(string name) => new NLogLogger(LogManager.GetLogger(string.Format("MySqlConnector.{0}", name)));
    }
}
