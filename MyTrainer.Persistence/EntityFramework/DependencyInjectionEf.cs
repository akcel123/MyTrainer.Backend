using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyTrainer.Application.Interfaces;

namespace MyTrainer.Persistence.EntityFramework;

public static class DependencyInjectionEf
{
    public static void AddPersistenceEf(this IServiceCollection services, string dbConf)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TrainingDbContextEf>();
        var options = optionsBuilder.UseNpgsql(dbConf).Options;
        services.AddScoped<ITrainingDbContext>(provider => new TrainingDbContextEf(options));
    }
}

