using MyTrainer.Domain;
namespace MyTrainer.Application;

public interface ITrainingRepository
{ 
    void Create(Training training);
    Training? Get(Guid guid);
    IEnumerable<Training> GetAllTrainings();
    void Update(Training training);
    void Delete(Guid guid);
}
