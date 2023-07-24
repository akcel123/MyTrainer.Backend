using MyTrainer.API.Models;
using MyTrainer.UnitTests.Infrastructure;

namespace MyTrainer.UnitTests.API;

public class GetTrainingDtoUnitTests
{
    [Fact]
    public void ToTraining_ReturnsCorrectTraining_WhenAtgumentIsCorrect()
    {
        // Arrange
        var training = MockTraining.Training;

        var getTrainingDto = new GetTrainingDto()
        {
            Id = training.Id,
            UserId = training.UserId,
            TrainerId = training.TrainerId,
            Name = training.Name,
            Description = training.Description,
            CreationDate = training.CreationDate.ToDateTime(new TimeOnly(0)),
            EditDate = training.EditDate?.ToDateTime(new TimeOnly(0)),
            IsCompleted = training.IsCompleted
        };

        // Act
        var dtoFromTraining = GetTrainingDto.FromTraining(training);

        // Assert
        Assert.Equal(getTrainingDto.CreationDate, dtoFromTraining.CreationDate);
        Assert.Equal(getTrainingDto.Description, dtoFromTraining.Description);
        Assert.Equal(getTrainingDto.EditDate, dtoFromTraining.EditDate);
        Assert.Equal(getTrainingDto.IsCompleted, dtoFromTraining.IsCompleted);
        Assert.Equal(getTrainingDto.Name, dtoFromTraining.Name);
        Assert.Equal(getTrainingDto.TrainerId, dtoFromTraining.TrainerId);
        Assert.Equal(getTrainingDto.UserId, dtoFromTraining.UserId);
        Assert.Equal(getTrainingDto.Id, dtoFromTraining.Id);
    }
}

