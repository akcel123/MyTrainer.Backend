using MyTrainer.Application.Exceptions;
using MyTrainer.Application.Extensions;
using MyTrainer.Application.Interfaces;
using MyTrainer.Domain;
using Npgsql;

namespace MyTrainer.Persistence;


public class TrainingDbContext: ITrainingDbContext
{
    readonly string _connectionString;
    private static readonly string _tableName = "trainings";

    public TrainingDbContext(string connectionString)
    {
        _connectionString = connectionString;

    }

    public IEnumerable<Training> GetAllTrainings()
    {
        List<Training> trainings = new();

        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sqlQuery = $"SELECT * FROM {_tableName} ";

            using var command = new NpgsqlCommand(sqlQuery, connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var training = new Training();
                training.Id = reader.GetGuid(0);
                training.UserId = reader.GetGuid(1);
                training.TrainerId = reader.GetGuid(2);
                training.Name = reader.IsDBNull(3) ? null : reader.GetString(3);
                training.Description = reader.GetString(4);

                var creationDate = reader.GetDateTime(5);
                DateTime? editDate = reader.IsDBNull(6) ? null : reader.GetDateTime(6);

                training.IsCompleted = reader.GetBoolean(7);
                training.CreationDate = creationDate.ToDateOnly();
                training.EditDate = editDate == null ? null : new DateOnly(editDate.Value.Year, editDate.Value.Month, editDate.Value.Day);
                trainings.Add(training);

            }
        }
        catch (NpgsqlException exception)
        {
            throw new DatabaseException(exception.Message);
        }

        return trainings;
    }

    public void Create(Training training)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sqlQuery = $"INSERT INTO {_tableName} " +
                              $"(id, user_id, trainer_id, name, description, creation_date, edit_date, is_completed) " +
                              $"VALUES " +
                              $"(@id, @user_id, @trainer_id, @training_name, @training_description, @creation_date, @edit_date, @is_completed)";

            using var command = new NpgsqlCommand(sqlQuery, connection);

            command.Parameters.AddWithValue("id", training.Id);
            command.Parameters.AddWithValue("user_id", training.UserId);
            command.Parameters.AddWithValue("trainer_id", training.TrainerId);
            command.Parameters.AddWithValue("training_name", training.Name != null ? training.Name : DBNull.Value);
            command.Parameters.AddWithValue("training_description", training.Description);
            command.Parameters.AddWithValue("creation_date", training.CreationDate);
            command.Parameters.AddWithValue("edit_date", DBNull.Value);
            command.Parameters.AddWithValue("is_completed", false);
            
            command.ExecuteNonQuery();

        }
        catch (NpgsqlException exception)
        {
            throw new DatabaseException(exception.Message);
        }
    }


    public void Delete(Guid guid)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sqlQuery = $"DELETE FROM {_tableName} " +
                              $"WHERE id = @id";
            using var command = new NpgsqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("id", guid);
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected <= 0)
            {
                throw new EntityNotFoundException();
            }
        }
        catch (NpgsqlException exception)
        {
            throw new DatabaseException(exception.Message);
        }

    }


    public Training? Get(Guid guid)
    {
        Training? training = null;

        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sqlQuery = $"SELECT * FROM {_tableName} " +
                              $"WHERE id = @id";
            using var command = new NpgsqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("id", guid);
            NpgsqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                training = new Training();
                training.Id = reader.GetGuid(0);
                training.UserId = reader.GetGuid(1);
                training.TrainerId = reader.GetGuid(2);
                training.Name = reader.IsDBNull(3) ? null : reader.GetString(3);
                training.Description = reader.GetString(4);

                var creationDate = reader.GetDateTime(5);
                DateTime? editDate = reader.IsDBNull(6) ? null : reader.GetDateTime(6);

                training.IsCompleted = reader.GetBoolean(7);
                training.CreationDate = new DateOnly(creationDate.Year, creationDate.Month, creationDate.Day);
                training.EditDate = editDate == null ? null : new DateOnly(editDate.Value.Year, editDate.Value.Month, editDate.Value.Day);
            }
        }
        catch (NpgsqlException exception)
        {
            throw new DatabaseException(exception.Message);
        }

        return training;
    }


    public void SaveContext()
    {
        //Данная функция ничего не делает, так как мы используем нативные запросы ADO.NET
        //Необходимость данной функции определяется интерфейсом
    }


    public void Update(Training training)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string sqlQuery = $"UPDATE {_tableName} " +
                              $"SET " +
                              $"user_id = @user_id, trainer_id = @trainer_id, name = @training_name, " +
                              $"description = @training_description, creation_date = @creation_date, " +
                              $"edit_date = @edit_date, is_completed = @is_completed " +
                              $"WHERE id = @id";

            using var command = new NpgsqlCommand(sqlQuery, connection);

            command.Parameters.AddWithValue("user_id", training.UserId);
            command.Parameters.AddWithValue("trainer_id", training.TrainerId);
            command.Parameters.AddWithValue("training_name", training.Name != null ? training.Name : DBNull.Value);
            command.Parameters.AddWithValue("training_description", training.Description);
            command.Parameters.AddWithValue("creation_date", training.CreationDate);
            command.Parameters.AddWithValue("edit_date", training.EditDate != null ? training.EditDate : DBNull.Value);
            command.Parameters.AddWithValue("is_completed", training.IsCompleted);

            command.Parameters.AddWithValue("id", training.Id);


            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected <= 0)
            {
                throw new EntityNotFoundException();
            }

        }
        catch (NpgsqlException exception)
        {
            throw new DatabaseException(exception.Message);
        }

    }

}