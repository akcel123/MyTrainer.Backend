using MyTrainer.API.Models;
using MyTrainer.UnitTests.Infrastructure;

namespace MyTrainer.UnitTests.API;

public class CreateTrainingDtoUnitTests
{

	[Fact]
	public void ToTraining_ReturnsCorrectTraining_WhenAtgumentIsCorrect()
	{
        // Arrange
        var training = MockTraining.Training;

        var createTrainingDto = new CreateTrainingDto()
        {
            Description = training.Description,
            Name = training.Name,
            TrainerId = training.TrainerId,
            UserId = training.UserId
        };

        // Act
        var trainingFromDto = createTrainingDto.ToTraining();

        // Assert
        Assert.Equal(training.CreationDate, trainingFromDto.CreationDate);
        Assert.Equal(training.Description, trainingFromDto.Description);
        Assert.Equal(training.EditDate, trainingFromDto.EditDate);
        Assert.Equal(training.IsCompleted, trainingFromDto.IsCompleted);
        Assert.Equal(training.Name, trainingFromDto.Name);
        Assert.Equal(training.TrainerId, trainingFromDto.TrainerId);
        Assert.Equal(training.UserId, trainingFromDto.UserId);
	}
}

