﻿using MyTrainer.Application.Extensions;
using MyTrainer.Domain;

namespace MyTrainer.API.Models;

public class CreateTrainingDto
{
    public Guid TrainerId { get; set; }     
    public Guid UserId { get; set; }           
    public string? Name { get; set; }      
    public string Description { get; set; } 

    public Training ToTraining()
        => new()
        {
            Id = Guid.NewGuid(),
            UserId = UserId,
            TrainerId = TrainerId,
            Name = Name,
            Description = Description,
            CreationDate = DateTime.Now.ToDateOnly(),
            EditDate = null,
            IsCompleted = false
        };


}

