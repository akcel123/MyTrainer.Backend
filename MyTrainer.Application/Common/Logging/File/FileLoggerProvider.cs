using Microsoft.Extensions.Logging;


namespace MyTrainer.Application.Common.Logging.File;

public class FileLoggerProvider: ILoggerProvider
{
    readonly string _filePath;
    readonly LogLevel _logLevel;

    public FileLoggerProvider(string filePath, LogLevel logLevel)
        => (_filePath, _logLevel) = (filePath, logLevel);

    // Создает и возвращает объект логгера
    public ILogger CreateLogger(string categoryName)
        => new FileLogger(_filePath, _logLevel);

    public void Dispose() { }
}
