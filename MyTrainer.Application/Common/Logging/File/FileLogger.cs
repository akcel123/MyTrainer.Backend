using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MyTrainer.Application.Common.Logging.File;

public class FileLogger: ILogger, IDisposable
{
    private readonly string _filePath;
    private readonly LogLevel _logLevel;

    static object _lock = new object();
    static int _numOfFile = 0;
    const int MaxFileSize = 64 * 1024 * 1024;

    public FileLogger(string filePath, LogLevel logLevel)
        => (_filePath, _logLevel) = (filePath, logLevel);

    public IDisposable BeginScope<TState>(TState state)
        => this;


    //Данный метод возвращает, будем ли мы использовать логгер или нет (с соответствующим уровнем) (явл-ся необязательным, поэтому проверку в методее Log осуществлять)
    public bool IsEnabled(LogLevel logLevel)
        => logLevel >= _logLevel;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        var timeNow = DateTime.Now;
        string logMessage;


        //Если существует экзепшн, добавь его в сообщение
        if (exception != null)
            logMessage = $"{logLevel} : {timeNow:G} - {formatter(state, exception)} - \n Method {exception?.TargetSite?.Name} in {exception?.TargetSite?.DeclaringType?.FullName}";
        else
            logMessage = $"{logLevel} : {timeNow:G} - {formatter(state, exception)}";


        lock (_lock)
        {

            if (!Directory.Exists(_filePath))
                Directory.CreateDirectory(_filePath);

            var filePathWithName = $"{_filePath}{timeNow:d} {_numOfFile}.txt";


            while (System.IO.File.Exists(filePathWithName) && new FileInfo(filePathWithName).Length > MaxFileSize)
            {
                _numOfFile++;
                filePathWithName = $"{_filePath}{timeNow:d} {_numOfFile}.txt";
            }


            System.IO.File.AppendAllText(filePathWithName, logMessage + Environment.NewLine);
        }
    }



    public void Dispose()
    {

    }
}
