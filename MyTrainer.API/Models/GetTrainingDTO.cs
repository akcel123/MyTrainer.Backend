using MyTrainer.Application.Extensions;
using MyTrainer.Domain;
using System.Xml;

namespace MyTrainer.API.Models;

public class GetTrainingDto
{
    public Guid Id { get; set; }           
    public Guid UserId { get; set; }        
    public Guid TrainerId { get; set; }     
    public string? Name { get; set; }       
    public string Description { get; set; } 
    public DateTime CreationDate { get; set; }
    public DateTime? EditDate { get; set; }
    public bool IsCompleted { get; set; }

    public static GetTrainingDto FromTraining(Training training)
        => new()
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

}

