using Microsoft.Extensions.DependencyInjection;
using MyTrainer.Application.Interfaces;

namespace MyTrainer.Persistence;


public static class DependencyInjection
{

    public static void AddPersistence(this IServiceCollection services, string dbConf)
    {
        services.AddScoped<ITrainingDbContext>(provider => new TrainingDbContext(dbConf));
    }
}
