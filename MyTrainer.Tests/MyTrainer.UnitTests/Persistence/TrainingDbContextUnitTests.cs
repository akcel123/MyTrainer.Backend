using System.Text.Json;
using MyTrainer.Application.Exceptions;
using MyTrainer.Domain;
using MyTrainer.Persistence;
using MyTrainer.UnitTests.Infrastructure;
using Npgsql;

namespace MyTrainer.UnitTests.Persistence;

public class TrainingDbContextUnitTests
{

    private static readonly string _tableName = "trainings";
    private readonly string connectionString = "";
    private Guid[] guids = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
    public TrainingDbContextUnitTests()
    {
        connectionString = GetConnectionString();
        CreateDb(connectionString);
    }

    [Fact]
    public void Create_ThrowAnyException_WhenConnectionStringIsEmpty()
    {
        // Arrange
        var dbContext = new TrainingDbContext("");

        // Assert
        Assert.ThrowsAny<Exception>(() => dbContext.Create(new Training()));
    }

    [Fact]
    public void Create_ThrowAnyException_WhenConnectionStringHasIncorrectFormat()
    {
        // Arrange
        var dbContext = new TrainingDbContext("123;123;123;");

        // Assert
        Assert.ThrowsAny<Exception>(() => dbContext.Create(new Training()));
    }

    [Fact]
    public void CreateAndGet_ShouldCreateAndGetEntityIntoDatabase_WhenArgumentIsCorrect()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var dbContext = new TrainingDbContext(connectionString);

        // Act
        dbContext.Create(MockTraining.WithId(guid));
        var training = dbContext.Get(guid);

        // Assert
        Assert.Equal(training.Id, guid);
        
    }

    [Fact]
    public void Update_ShouldUpdateTraining_WhenArgumentIsCorrect()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var dbContext = new TrainingDbContext(connectionString);
        var training = MockTraining.WithId(guid);
        dbContext.Create(training);

        // Act
        training.IsCompleted = !training.IsCompleted;
        training.Name = "BarBaz";
        training.Description = "FooAndFoo";
        dbContext.Update(training);
        var updatedTraining = dbContext.Get(guid);

        // Assert
        Assert.Equal(training.Id, updatedTraining.Id);
        Assert.Equal(training.IsCompleted, updatedTraining.IsCompleted);
        Assert.Equal(training.Name, updatedTraining.Name);
        Assert.Equal(training.Description, updatedTraining.Description);
        Assert.Equal(training.CreationDate, updatedTraining.CreationDate);
    }

    [Fact]
    public void Update_ShouldThrowsEntityNotFoundException_WhenIdNotFound()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var dbContext = new TrainingDbContext(connectionString);
        var training = MockTraining.WithId(guid);
        dbContext.Create(training);
        // Act
        training.Id = Guid.NewGuid();
        // Assert
        Assert.Throws<EntityNotFoundException>(() => dbContext.Update(training));
    }

    [Fact]
    public void Get_ShouldReturnNull_WhenIdNotFound()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var dbContext = new TrainingDbContext(connectionString);
        // Act
        var training = dbContext.Get(guid);
        // Assert
        Assert.Null(training);
    }

    [Fact]
    public void Delete_ShouldThrowsEntityNotFoundException_WhenIdNotFound()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var dbContext = new TrainingDbContext(connectionString);
        // Assert
        Assert.Throws<EntityNotFoundException>(() => dbContext.Delete(guid));
    }

    private static string GetConnectionString()
    {

        string configFilePath = "../../../appsettings.json";
        string jsonContent = File.ReadAllText(configFilePath);

        var jsonOptions = new JsonSerializerOptions
        {
            ReadCommentHandling = JsonCommentHandling.Skip
        };
        var jsonObject = JsonSerializer.Deserialize<JsonElement>(jsonContent, jsonOptions);

        var connectionStringElement = jsonObject
            .GetProperty("ConnectionStrings")
            .GetProperty("unit_tests_db")
            .GetString();


        var connectionString = connectionStringElement;
        return connectionString;
    }

    private static void CreateDb(string connectionString)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            string createDatabaseQuery = $"CREATE TABLE IF NOT EXISTS {_tableName} (" +
                                         $"id UUID PRIMARY KEY," +
                                         $"user_id UUID NOT NULL," +
                                         $"trainer_id UUID NOT NULL," +
                                         $"name VARCHAR(100)," +
                                         $"description TEXT NOT NULL," +
                                         $"creation_date DATE NOT NULL," +
                                         $"edit_date DATE," +
                                         $"is_completed BOOLEAN NOT NULL);";
            using var command = new NpgsqlCommand(createDatabaseQuery, connection);
            command.ExecuteNonQuery();
        }

        
    }
}

