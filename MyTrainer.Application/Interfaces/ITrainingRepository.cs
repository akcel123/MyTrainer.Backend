using MyTrainer.Application.Interfaces;
using MyTrainer.Domain;
using System;
namespace MyTrainer.Application;

public interface ITrainingRepository
{ 
    void Create(Training training);
    Training? Get(Guid guid);
    IEnumerable<Training> GetAllTrainings();
    void Update(Training training);
    void Delete(Guid guid);
    //TODO: до сих пор не знаю что делать с этим методом
    void Save();
}
