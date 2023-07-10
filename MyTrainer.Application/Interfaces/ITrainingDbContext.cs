﻿using System;
using MyTrainer.Domain;

namespace MyTrainer.Application.Interfaces;

public interface ITrainingDbContext
{
    void Create(Training training);
    Training? Get(Guid guid);
    IEnumerable<Training> Trainings { get; }
    void Update(Training training);
    void Delete(Guid guid);
    void SaveContext();

}

