using MyTrainer.API.Controllers;
using MyTrainer.Domain;
using MyTrainer.UnitTests.Infrastructure;
using MyTrainer.Application.Extensions;
using MyTrainer.API.Models;
using MyTrainer.Application.Exceptions;
using System;

namespace MyTrainer.UnitTests.API;


// В мпетоде получения всех тренировок получить все тренировки и сравнить ID
public class TrainingControllerUnitTests
{
    [Fact]
    public void GetTrainings_ReturnsAllTrainings()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var controller = new TrainingsController(new MockRepository(dbContext), new MockLogger());
        var ids = dbContext.Guids;

        // Act
        var trainings = controller.GetTrainings();

        // Assert
        foreach (var id in ids)
            Assert.Contains(trainings, t => t.Id == id);
    }


    [Fact]
    public void GetTraining_ShouldReturnNull_WhenArgumentWrong()
	{
		// Arrange
		var controller = new TrainingsController(new MockRepository(new MockDbContext()), new MockLogger());

		// Act
		var training = controller.GetTraining(Guid.NewGuid());

		// Assert
		Assert.Null(training);
	}

    [Fact]
    public void GetTraining_ShouldReturnGetTrainingDto_WhenArgumentIsCorrect()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var controller = new TrainingsController(new MockRepository(dbContext), new MockLogger());
        var id = dbContext.Guids.First();
        // Act
        var training = controller.GetTraining(id);

        // Assert
        Assert.NotNull(training);
        Assert.Equal(training!.Id, dbContext.Trainings.FindAll(t => t.Id == id).First().Id);
    }

    [Fact]
	public void CreateTraining_ShouldReturnGetTrainingDto_WhenCorrectArgument()
	{
        // Arrange
        var controller = new TrainingsController(new MockRepository(new MockDbContext()), new MockLogger());
        var createTrainingDto = new CreateTrainingDto()
        {
            Description = "Bar",
            Name = null,
            TrainerId = new Guid("5f01f10a-2371-483b-a8ac-692332eb7ab8"),
            UserId = new Guid("03f9fb7f-514b-44c7-a1b7-de507ec506c9")
        };

        // Act
        var getTrainingDto = controller.CreateTraining(createTrainingDto);

        // Assert
        Assert.Equal(createTrainingDto.UserId, getTrainingDto.UserId);
        Assert.Equal(createTrainingDto.TrainerId, getTrainingDto.TrainerId);
        Assert.Equal(createTrainingDto.Name, getTrainingDto.Name);
        Assert.Equal(createTrainingDto.Description, getTrainingDto.Description);
        Assert.Null(getTrainingDto.EditDate);
        Assert.False(getTrainingDto.IsCompleted);
        Assert.NotEqual(getTrainingDto.CreationDate, new DateTime());
    }

    [Fact]
    public void DeleteTraining_ThrowEntityNotFoundException_WhenIdIsWrong()
    {
        // Arrange
        var controller = new TrainingsController(new MockRepository(new MockDbContext()), new MockLogger());

        // Assert
        Assert.Throws<EntityNotFoundException>(() => controller.DeleteTraining(Guid.NewGuid()));
    }

    [Fact]
    public void DeleteTraining_ShouldDeleteInDatabase()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var controller = new TrainingsController(new MockRepository(dbContext), new MockLogger());
        var id = dbContext.Guids.First();
        // Act
        controller.DeleteTraining(id);
        var trainings = dbContext.Trainings;
        // Assert
        Assert.DoesNotContain(trainings, t => t.Id == id);
    }

    [Fact]
    public void UpdateTraining_ShouldReturnGetTrainingDto_WhenArgumentIsCorrect()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var controller = new TrainingsController(new MockRepository(dbContext), new MockLogger());
        var id = dbContext.Guids.First();
        var training = dbContext.Trainings.Find(t => t.Id == id);
        // Act
        var updateTrainingDto = new UpdateTrainingDTO()
        {
            CreationDate = training.CreationDate.ToDateTime(new TimeOnly(0)),
            Description = "Baz",
            IsCompleted = !training.IsCompleted,
            Name = "Bar",
            TrainerId = new Guid("625fd466-fb3a-49e5-8668-42f2e70949a5"),
            UserId = new Guid("fb8dcb5a-8789-4d25-bc3f-5cdbef9531b4")
        };

        var getTrainingDto = controller.UpdateTraining(updateTrainingDto, id);

        // Assert
        Assert.Equal(getTrainingDto.CreationDate, updateTrainingDto.CreationDate);
        Assert.Equal(getTrainingDto.Description, updateTrainingDto.Description);
        Assert.Equal(getTrainingDto.Id, id);
        Assert.Equal(getTrainingDto.IsCompleted, updateTrainingDto.IsCompleted);
        Assert.Equal(getTrainingDto.Name, updateTrainingDto.Name);
        Assert.Equal(getTrainingDto.TrainerId, updateTrainingDto.TrainerId);
        Assert.Equal(getTrainingDto.UserId, updateTrainingDto.UserId);
    }

    [Fact]
    public void UpdateTraining_ShouldUpdateTraining_WhenArgumentIsCorrect()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var controller = new TrainingsController(new MockRepository(dbContext), new MockLogger());
        var id = dbContext.Guids.First();
        var training = dbContext.Trainings.Find(t => t.Id == id);
        // Act
        var updateTrainingDto = new UpdateTrainingDTO()
        {
            CreationDate = training.CreationDate.ToDateTime(new TimeOnly(0)),
            Description = "Baz",
            IsCompleted = !training.IsCompleted,
            Name = "Bar",
            TrainerId = new Guid("625fd466-fb3a-49e5-8668-42f2e70949a5"),
            UserId = new Guid("fb8dcb5a-8789-4d25-bc3f-5cdbef9531b4")
        };

        controller.UpdateTraining(updateTrainingDto, id);
        training = dbContext.Trainings.Find(t => t.Id == id);
        
        // Assert
        Assert.Equal(training.CreationDate, updateTrainingDto.CreationDate.ToDateOnly());
        Assert.Equal(training.Description, updateTrainingDto.Description);
        Assert.Equal(training.Id, id);
        Assert.Equal(training.IsCompleted, updateTrainingDto.IsCompleted);
        Assert.Equal(training.Name, updateTrainingDto.Name);
        Assert.Equal(training.TrainerId, updateTrainingDto.TrainerId);
        Assert.Equal(training.UserId, updateTrainingDto.UserId);
    }

    [Fact]
    public void UpdateTraining_ThrowEntityNotFoundException_WhenIdIsWrong()
    {
        // Arrange
        var dbContext = new MockDbContext();
        var controller = new TrainingsController(new MockRepository(dbContext), new MockLogger());
        // Act

        // Assert
        Assert.Throws<EntityNotFoundException>(() => controller.UpdateTraining(new UpdateTrainingDTO(), Guid.NewGuid()));
    }

}

