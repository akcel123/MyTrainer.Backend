
using MyTrainer.Application.Extensions;
using MyTrainer.Domain;
using System;

namespace MyTrainer.API.Models;

public class UpdateTrainingDTO
{ 
    public Guid UserId { get; set; }
    public Guid TrainerId { get; set; }
    public string? Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? EditDate { get; set; }
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
            EditDate = EditDate?.ToDateOnly(),
            IsCompleted = IsCompleted
        };

}
