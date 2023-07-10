using System;
namespace MyTrainer.Application.Structs;

public struct DbConnectionParameters
{
    public string ServerAddress { get; set; }
    public string Port { get; set; }
    public string DatabaseName { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }
    
    
}
