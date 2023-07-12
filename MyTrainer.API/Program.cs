using Microsoft.AspNetCore.Builder;
using MyTrainer.Application;
using MyTrainer.Persistence;


var builder = WebApplication.CreateBuilder(args);

//������ ���� ��������� �������� �������� ��� ���� Persistence
//FIXME: forse unwrapping
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("training_db")!);
builder.Services.AddScoped<ITrainingRepository, PostgreSqlTrainingRepository>();
builder.Services.AddControllersWithViews();


var app = builder.Build();


app.UseRouting();
app.MapGet("HeartBeatTest", () => "��� � �������");
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
