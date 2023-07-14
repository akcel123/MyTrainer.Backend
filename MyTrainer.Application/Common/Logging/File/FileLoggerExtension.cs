using Microsoft.Extensions.Logging;

namespace MyTrainer.Application.Common.Logging.File;

public static class FileLoggerExtension
{
    public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath, LogLevel logLevel)
    {
        builder.AddProvider(new FileLoggerProvider(filePath, logLevel));
        return builder;
    }
}
