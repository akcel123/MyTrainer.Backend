using MyTrainer.API.Middleware;
using MyTrainer.Application;
using MyTrainer.Application.Common.Logging.File;
using MyTrainer.Persistence;
using MyTrainer.Persistence.EntityFramework;

var builder = WebApplication.CreateBuilder(args);



//��������� ������ � ������� ������� critical � ����
builder.Logging.AddFile("../logging_file/critical/", LogLevel.Error);




//������ ���� ��������� �������� �������� ��� ���� Persistence
//FIXME: forse unwrapping
//builder.Services.AddPersistence(builder.Configuration.GetConnectionString("training_db")!);
builder.Services.AddPersistenceEf(builder.Configuration.GetConnectionString("training_db")!);
builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();


builder.Services.AddControllersWithViews();


var app = builder.Build();

app.UseRouting();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapGet("HeartBeatTest", () => "��� � �������");
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
