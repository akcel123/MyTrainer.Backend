using MyTrainer.Application;
using MyTrainer.Application.Interfaces;
using MyTrainer.Domain;

namespace MyTrainer.UnitTests.Infrastructure;

public class MockRepository: ITrainingRepository
{
    readonly ITrainingDbContext _dbContext;

    public MockRepository(ITrainingDbContext dbContext)
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

    public void Save()
    {
        _dbContext.SaveContext();
    }

    public void Update(Training training)
    {
        _dbContext.Update(training);
    }
}

