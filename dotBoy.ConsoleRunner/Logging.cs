using System;

using NLog;
using NLog.Targets;
using NLog.Config;

namespace DotBoy.ConsoleRunner
{
    internal static class Logging
    {
        internal static void TryConfigure(string logFile, string logLayout)
        {
            try
            {
                Configure(logFile, logLayout);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(
                    $"There as an error configuring logging: {ex.Message}");
                Console.Error.WriteLine(
                    $"StackTrace:{Environment.NewLine}{ex.StackTrace}");
            }
        }

        static void Configure(string logFile, string logLayout)
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget();
            consoleTarget.Layout = logLayout;

            var fileTarget = new FileTarget();
            fileTarget.Layout = logLayout;
            fileTarget.FileName = logFile;
            fileTarget.KeepFileOpen = true;

            var consoleLoggingRule =
                new LoggingRule("*", LogLevel.Trace, consoleTarget);
            var fileLoggingRule =
                new LoggingRule("*", LogLevel.Trace, fileTarget);

            config.AddTarget("console", consoleTarget);
            config.AddTarget("file", fileTarget);

            config.LoggingRules.Add(consoleLoggingRule);
            config.LoggingRules.Add(fileLoggingRule);

            LogManager.Configuration = config;
        }

        public const string NLOG_LAYOUT = @"${date:format=HH\:mm\:ss} ${logger} - ${message}";
        public const string NLOG_FILE = "${basedir}/dotboy.log.txt";
    }
}
