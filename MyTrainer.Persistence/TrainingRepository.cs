using MyTrainer.Application;
using MyTrainer.Application.Interfaces;
using MyTrainer.Domain;

namespace MyTrainer.Persistence;

public class TrainingRepository: ITrainingRepository
{
    readonly ITrainingDbContext _dbContext;
		public TrainingRepository(ITrainingDbContext dbContext)
		    => _dbContext = dbContext;

    public void Create(Training training)
    {
        _dbContext.Create(training);
        _dbContext.SaveContext();
    }

    public void Delete(Guid guid)
    {
        _dbContext.Delete(guid);
        _dbContext.SaveContext();
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
        _dbContext.SaveContext();
    }



}

