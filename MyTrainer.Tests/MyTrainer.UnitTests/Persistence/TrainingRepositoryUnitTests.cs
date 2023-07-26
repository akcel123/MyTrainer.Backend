using MyTrainer.Application.Exceptions;
using MyTrainer.Domain;
using MyTrainer.Persistence;
using MyTrainer.UnitTests.Infrastructure;
using Xunit;

namespace MyTrainer.UnitTests.Persistence;

public class TrainingRepositoryUnitTests
{
    [Fact]
    public void Create_TrainingDidCreated_WhenArgumentIsCorrect()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var repository = new TrainingRepository(dbContext);
        var training = MockTraining.Training;

        // Act
        repository.Create(training);
        var createdTraining = dbContext.Trainings.FindLast(trainingInList => trainingInList.Id == training.Id);
        // Assert
        Assert.True(IsTrainingsEqual(createdTraining!, training));
    }

    [Fact]
    public void GetTraining_ReturnsTraining_WhenIdIsValid()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var repository = new TrainingRepository(dbContext);
        var training = dbContext.Trainings.FindLast(trainingInList => trainingInList.Id == dbContext.Guids.First());
        // Act
        var gottenTraining = repository.Get(dbContext.Guids.First());
        // Assert
        Assert.True(IsTrainingsEqual(gottenTraining!, training!));
    }

    [Fact]
    public void GetTrainings_ReturnsNull_WhenTrainingNotFound()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var repository = new TrainingRepository(dbContext);
        // Act
        var gottenTraining = repository.Get(Guid.NewGuid());
        // Assert
        Assert.Null(gottenTraining);
    }

    [Fact]
    public void DeleteTraining_ThrowException_WhenTrainingNotFound()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var repository = new TrainingRepository(dbContext);
        // Act

        // Assert
        Assert.Throws<EntityNotFoundException>(() => repository.Delete(Guid.NewGuid()));
    }

    [Fact]
    public void DeleteTraining_DeletedTraining_WhenIdIsFounded()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var repository = new TrainingRepository(dbContext);
        var guid = dbContext.Guids.First();
        // Act
        repository.Delete(guid);

        // Assert
        Assert.Null(dbContext.Trainings.Find(training => training.Id == guid));
    }

    [Fact]
    public void UpdateTraining_CorrectUpdating_WhenArgumentIsCorrect()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var repository = new TrainingRepository(dbContext);
        var guid = dbContext.Guids.First();
        var training = dbContext.Trainings.Find(t => t.Id == guid);
        // Act
        var newTraining = new Training()
        {
            CreationDate = training.CreationDate,
            Description = "Baz",
            EditDate = training.EditDate,
            Id = training.Id,
            IsCompleted = !training.IsCompleted,
            Name = "bar",
            TrainerId = new Guid("625fd466-fb3a-49e5-8668-42f2e70949a5"),
            UserId = new Guid("fb8dcb5a-8789-4d25-bc3f-5cdbef9531b4"),
        };

        repository.Update(newTraining);
        var updatedTraining = dbContext.Trainings.Find(t => t.Id == guid);
        // Assert
        Assert.True(IsTrainingsEqual(newTraining, updatedTraining!));
    }

    [Fact]
    public void UpdateTraining_ThrowEntityNotFoundException_WhenGuidNotFound()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var repository = new TrainingRepository(dbContext);
        var guid = dbContext.Guids.First();
        var training =  dbContext.Trainings.Find(t => t.Id == guid);
        // Act
        var newTraining = new Training()
        {
            CreationDate = training.CreationDate,
            Description = "Baz",
            EditDate = training.EditDate,
            Id = Guid.NewGuid(),
            IsCompleted = !training.IsCompleted,
            Name = "bar",
            TrainerId = new Guid("625fd466-fb3a-49e5-8668-42f2e70949a5"),
            UserId = new Guid("fb8dcb5a-8789-4d25-bc3f-5cdbef9531b4"),
        };

        // Assert
        Assert.Throws<EntityNotFoundException>(() => repository.Update(newTraining));
    }

    [Fact]
    public void GetAllTraining_ReturnsAllTrainings()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var repository = new TrainingRepository(dbContext);
        var trainings = dbContext.Trainings;
        // Act
        var gottedTrainings = repository.GetAllTrainings().ToList();

        // Assert
        for (int i = 0; i < trainings.Count; i++)
            Assert.True(IsTrainingsEqual(trainings[i], gottedTrainings[i]));
    }




    private static bool IsTrainingsEqual(Training f, Training s)
        => f.Id == s.Id &&
            f.CreationDate == s.CreationDate &&
            f.Description == s.Description &&
            f.EditDate == s.EditDate &&
            f.IsCompleted == s.IsCompleted &&
            f.Name == s.Name &&
            f.TrainerId == s.TrainerId &&
            f.UserId == s.UserId;
}

