using MyTrainer.Application;
using MyTrainer.Application.Interfaces;
using MyTrainer.Domain;
using System;

namespace MyTrainer.Persistence;

public class TrainingRepository: ITrainingRepository
{
    readonly ITrainingDbContext _dbContext;
		public TrainingRepository(ITrainingDbContext dbContext)
		    => _dbContext = dbContext;

    public void Create(Training training)
    {
        _dbContext.Create(training);
    }

    public void Delete(Guid guid)
    {
        _dbContext.Delete(guid);
    }


    public Training? Get(Guid guid)
    {
        return _dbContext.Get(guid);
    }

    public IEnumerable<Training> GetAllTrainings()
    {
        return _dbContext.GetAllTrainings();
    }
    public void Update(Training training)
    {
        _dbContext.Update(training);
    }

    public void Save()
    {
        _dbContext.SaveContext();
    }


}

