using MyTrainer.Application.Exceptions;
using MyTrainer.Application.Extensions;
using MyTrainer.Application.Interfaces;
using MyTrainer.Domain;

namespace MyTrainer.UnitTests.Infrastructure;

public class MockDbContext: ITrainingDbContext
{
    
    public readonly Guid[] Guids = new[] {
        new Guid("51fc083b-5200-418b-8bb3-ae7eede71166"),
        new Guid("f03bd56f-c52b-4eac-a4f0-8db225ee0d96"),
        new Guid("cfbb2100-3794-4da7-a509-eaccd30ba65b")
    };

    bool isTrainingCreated = false;


    public List<Training> Trainings = new();

	public MockDbContext()
	{
        for (int i = 0; i < 3; i++)
        {
            var training = new Training()
            {
                CreationDate = DateTime.Now.ToDateOnly(),
                IsCompleted = false,
                Id = Guids[i],
                TrainerId = new Guid("2127f3b0-4d7e-4d35-ab30-0c926dcc5795"),
                UserId = new Guid("2127f3b0-4d7e-4d35-ab30-0c926dcc5795"),
                Description = "foo",
                EditDate = null,
                Name = "baz"
            };
            Trainings.Add(training);
        }

	}

    public void Create(Training training)
    {
        Trainings.Add(training);
    }

    public void Delete(Guid guid)
    {
        var deletedList = Trainings.RemoveAll(training => training.Id == guid);
        if (deletedList == 0)
            throw new EntityNotFoundException();
    }

    public Training? Get(Guid guid)
    {
        return Trainings.Find(training => training.Id == guid);
    }

    public IEnumerable<Training> GetAllTrainings()
    {
        return Trainings;
    }

    public void SaveContext()
    {
        
    }

    public void Update(Training training)
    {
        var index = Trainings.FindIndex(training1 => training1.Id == training.Id);
        if (index == -1)
            throw new EntityNotFoundException();
        Trainings[index] = training;
    }


}

