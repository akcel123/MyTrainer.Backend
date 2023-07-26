using MyTrainer.API.Models;
using MyTrainer.UnitTests.Infrastructure;

namespace MyTrainer.UnitTests.API;

public class UpdateTrainingDtoUnitTests
{
    [Fact]
    public void ToTraining_ReturnsCorrectTraining_WhenAtgumentIsCorrect()
    {
        // Arrange
        var training = MockTraining.Training;

        var updateTrainingDto = new UpdateTrainingDTO()
        {
            CreationDate = training.CreationDate.ToDateTime(new TimeOnly(0)),
            IsCompleted = training.IsCompleted,
            Description = training.Description,
            Name = training.Name,
            TrainerId = training.TrainerId,
            UserId = training.UserId
        };

        // Act
        var trainingFromDto = updateTrainingDto.ToTraining(training.Id);

        // Assert
        Assert.Equal(training.CreationDate, trainingFromDto.CreationDate);
        Assert.Equal(training.Description, trainingFromDto.Description);
        Assert.Equal(training.IsCompleted, trainingFromDto.IsCompleted);
        Assert.Equal(training.Name, trainingFromDto.Name);
        Assert.Equal(training.TrainerId, trainingFromDto.TrainerId);
        Assert.Equal(training.UserId, trainingFromDto.UserId);
        Assert.Equal(training.Id, trainingFromDto.Id);
    }
}

