using MyTrainer.Application.Extensions;
using MyTrainer.Domain;

namespace MyTrainer.API.Models;

//TODO: Необходимо подумать, как сделать CreationDate нередактированным. Текущая реализация позволяет его отредактировать на стороне клиента
public class UpdateTrainingDTO
{ 
    public Guid UserId { get; set; }
    public Guid TrainerId { get; set; }
    public string? Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsCompleted { get; set; }

    public Training ToTraining(Guid guid)
        => new()
        {
            Id = guid,
            UserId = UserId,
            TrainerId = TrainerId,
            Name = Name,
            Description = Description,
            CreationDate = CreationDate.ToDateOnly(),
            EditDate = DateTime.Now.ToDateOnly(),
            IsCompleted = IsCompleted
        };
}
