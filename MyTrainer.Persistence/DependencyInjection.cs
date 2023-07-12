using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTrainer.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrainer.Persistence;


public static class DependencyInjection
{

    public static void AddPersistence(this IServiceCollection services, string dbConf)
    {
        services.AddScoped<ITrainingDbContext>(provider => new TrainingDbContext(dbConf));
        
    }
}
