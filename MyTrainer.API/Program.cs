using MyTrainer.Application;
using MyTrainer.Application.Common.Logging.File;
using MyTrainer.Persistence;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);



//Добавляем логгер с записью событий critical в фалй
builder.Logging.AddFile("../logging_file/critical/", LogLevel.Critical);




//Строка ниже добавляет инъекцию сервисов для слоя Persistence
//FIXME: forse unwrapping
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("training_db")!);
builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();


builder.Services.AddControllersWithViews();


var app = builder.Build();


app.UseRouting();
app.MapGet("HeartBeatTest", () => "Все в порядке");
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
