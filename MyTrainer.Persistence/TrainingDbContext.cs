using System;
using System.Collections.Generic;
using System.Xml.Linq;
using MyTrainer.Application.Interfaces;
using MyTrainer.Application.Structs;
using MyTrainer.Domain;
using Npgsql;

namespace MyTrainer.Persistence;


public class TrainingDbContext: ITrainingDbContext
{
    readonly string _connectionString;
    readonly NpgsqlConnection _connection;
    private static string _tableName = "trainings";
    

    public TrainingDbContext(DbConnectionParameters connectionParameters)
    {
        _connectionString = $"Server={connectionParameters.ServerAddress};" +
                            $"Port={connectionParameters.Port};" +
                            $"Database={connectionParameters.DatabaseName};" +
                            $"User Id={connectionParameters.UserId};" +
                            $"Password={connectionParameters.Password};";

        _connection = new NpgsqlConnection(_connectionString);
        
    }

/*
 * 
 * training_id int NOT NULL,
   user_id int NOT NULL,
   trainer_id int NOT NULL,
   training_name varchar(64),
   training_description text NOT NULL,
   creation_date date NOT NULL,
   edit_date date,
   is_completed bool NOT NULL
 */

    //TODO: Это свойство можем поменять, чтобы при обращении не получать каждый раз большую выборку, а сделать получение в отдельном методе
    public IEnumerable<Training> Trainings
    {
        get
        {
            IEnumerable <Training> trainings = new List<Training>();
            try
            {
                _connection.Open();
                string sqlQuery = $"SELECT * FROM {_tableName} ";

                var command = new NpgsqlCommand(sqlQuery, _connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var training = new Training()
                    {
                        Id = reader.GetGuid(0),
                        UserId = reader.GetGuid(1),
                        TrainerId = reader.GetGuid(2),
                        Name = reader.GetString(3),
                        Description = reader.GetString(4),
                        CreationDate = new DateOnly(reader.GetDateTime(5)), //FIXME: Эта строка неправильная, подумать как реализовать
                        EditDate = new DateOnly(reader.GetDateTime(6)),       //FIXME: то же самое
                        IsCompleted = reader.GetBoolean(7)
                    };

                    trainings.Append(training);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Возникла ошибка при получении значения: {exception}");
            }
            finally
            {
                _connection.Close();
            }

            return trainings;
        }
    }

    public void Create(Training training)
    {
        try
        {
            _connection.Open();
            string sqlQuery = $"INSERT INTO {_tableName} " +
                              $"(training_id, user_id, trainer_id, training_name, training_description, creation_date, edit_date, is_completed) " +
                              $"VALUES " +
                              $"(@training_id, @user_id, @trainer_id, @training_name, @training_description, @creation_date, @edit_date, @is_completed)";

            var command = new NpgsqlCommand(sqlQuery, _connection);
            command.Parameters.AddWithValue("training_id", training.Id);
            command.Parameters.AddWithValue("user_id", training.UserId);
            command.Parameters.AddWithValue("trainer_id", training.TrainerId);
            command.Parameters.AddWithValue("training_name", training.Name);
            command.Parameters.AddWithValue("training_description", training.Description);
            command.Parameters.AddWithValue("creation_date", training.CreationDate); //FIXME: параметр даты
            command.Parameters.AddWithValue("edit_date", null); //FIXME: как правильно???
            command.Parameters.AddWithValue("is_completed", false);

            command.ExecuteNonQuery();

        }
        catch (Exception exception)
        {
            Console.WriteLine($"Возникла ошибка {exception}");
        }
        finally
        {
            _connection.Close();
        }
    }


    public void Delete(Guid guid)
    {
        try
        {
            _connection.Open();
            string sqlQuery = $"DELETE FROM {_tableName} " +
                              $"WHERE id = @id";
            var command = new NpgsqlCommand(sqlQuery, _connection);
            command.Parameters.AddWithValue("id", guid);
            int rowsAffected = command.ExecuteNonQuery();

            //TODO: реализовать проверку успешности удаления, возможно нужно отредактировать функцию Delete

        }
        catch (Exception exception)
        {
            Console.WriteLine($"Возникла ошибка при получении значения: {exception}");
        }
        finally
        {
            _connection.Close();
        }
    }


    public Training? Get(Guid guid)
    {
        Training? training = null;
        try
        {
            _connection.Open();
            string sqlQuery = $"SELECT * FROM {_tableName} " +
                              $"WHERE id = @id";
            var command = new NpgsqlCommand(sqlQuery, _connection);
            command.Parameters.AddWithValue("id", guid);
            NpgsqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                training = new Training()
                {
                    Id = reader.GetGuid(0),
                    UserId = reader.GetGuid(1),
                    TrainerId = reader.GetGuid(2),
                    Name = reader.GetString(3),
                    Description = reader.GetString(4),
                    CreationDate = new DateOnly( reader.GetDateTime(5)), //FIXME: Эта строка неправильная, подумать как реализовать
                    EditDate = new DateOnly( reader.GetDateTime(6)),       //FIXME: то же самое
                    IsCompleted = reader.GetBoolean(7)
                };
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Возникла ошибка при получении значения: {exception}");
        }
        finally
        {
            _connection.Close();
        }

        return training;
    }


    public void SaveContext()
    {
        //TODO: а тут вообще что-то необходимо в случае работы с нативными запросами?
        throw new NotImplementedException();
    }


    public void Update(Training training)
    {
        try
        {
            _connection.Open();
            string sqlQuery = $"UPDATE {_tableName} " +
                              $"(user_id, trainer_id, training_name, training_description, creation_date, edit_date, is_completed) " +
                              $"SET " +
                              $"(@user_id, @trainer_id, @training_name, @training_description, @creation_date, @edit_date, @is_completed) " +
                              $"WHERE training_id = @training_id";

            var command = new NpgsqlCommand(sqlQuery, _connection);

            command.Parameters.AddWithValue("user_id", training.UserId);
            command.Parameters.AddWithValue("trainer_id", training.TrainerId);
            command.Parameters.AddWithValue("training_name", training.Name);
            command.Parameters.AddWithValue("training_description", training.Description);
            command.Parameters.AddWithValue("creation_date", training.CreationDate); //FIXME: параметр даты
            command.Parameters.AddWithValue("edit_date", training.EditDate); //FIXME: как правильно???
            command.Parameters.AddWithValue("is_completed", training.IsCompleted);

            command.Parameters.AddWithValue("training_id", training.Id);

            //TODO: тут можно реализовать проверку на успешность операции
            int rowsAffected = command.ExecuteNonQuery();

        }
        catch (Exception exception)
        {
            Console.WriteLine($"Возникла ошибка при обновлении события {exception}");
        }
        finally
        {
            _connection.Close();
        }
    }

    


}

