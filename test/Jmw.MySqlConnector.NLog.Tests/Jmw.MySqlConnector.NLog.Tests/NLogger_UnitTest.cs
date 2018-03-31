namespace Jmw.MySqlConnector.NLog.Tests
{
    using global::MySqlConnector.Logging;
    using MySql.Data.MySqlClient;
    using Xunit;

    /// <summary>
    /// <see cref="NLogLoggerProvider"/> unit tests;
    /// </summary>
    public class NLogger_UnitTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NLogger_UnitTest"/> class.
        /// </summary>
        public NLogger_UnitTest()
        {
            var config = new global::NLog.Config.LoggingConfiguration();

            var logDebugger = new global::NLog.Targets.DebuggerTarget() { Name = "debugger" };

            config.LoggingRules.Add(new global::NLog.Config.LoggingRule("*", global::NLog.LogLevel.Debug, logDebugger));

            global::NLog.LogManager.Configuration = config;

            MySqlConnectorLogManager.Provider = new NLogLoggerProvider();
        }

        [Fact]
        public void Test1()
        {
            using (var connection = new MySqlConnection())
            {
                var builder = new MySqlConnectionStringBuilder();

                builder.Server = "localhost";
                builder.Database = "mysql";
                builder.UserID = "root";
                builder.Password = "root";

                connection.ConnectionString = builder.ToString();

                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var command = connection.CreateCommand();

                    command.Connection = connection;
                    command.Transaction = transaction;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM mysql.user WHERE user=@user;";

                    var param = command.CreateParameter();

                    param.ParameterName = "user";
                    param.Value = "root";

                    command.Parameters.Add(param);

                    using (var reader = command.ExecuteReader())
                    {
                        Assert.True(reader.HasRows);
                    }

                    transaction.Commit();
                }

                connection.Close();
            }
        }
    }
}
