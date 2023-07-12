using Microsoft.AspNetCore.Builder;
using MyTrainer.Application;
using MyTrainer.Persistence;


var builder = WebApplication.CreateBuilder(args);

//Строка ниже добавляет инъекцию сервисов для слоя Persistence
//FIXME: forse unwrapping
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("training_db")!);
builder.Services.AddScoped<ITrainingRepository, PostgreSqlTrainingRepository>();
builder.Services.AddControllersWithViews();


var app = builder.Build();


app.UseRouting();
app.MapGet("HeartBeatTest", () => "Все в порядке");
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
