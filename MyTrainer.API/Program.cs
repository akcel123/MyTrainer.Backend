using MyTrainer.Persistence;


var builder = WebApplication.CreateBuilder(args);

//������ ���� ��������� �������� �������� ��� ���� Persistence
//FIXME: forse unwrapping
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("training_db")!);

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();
