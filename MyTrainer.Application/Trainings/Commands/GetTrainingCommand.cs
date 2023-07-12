using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrainer.Application.Trainings.Commands;

public class GetTrainingCommand
{
    public Guid Id { get; set; }            // ID самой тренировки
    public Guid UserId { get; set; }        // ID пользователя, которому предназначена тренировка
    public Guid TrainerId { get; set; }     // Вот это свойство под вопросом (ID тренера, который написал тренировку)
    public string? Name { get; set; }       // Название тренировки (например, "силовая тренировка ног")
    public string Description { get; set; } // Определяет всю тренировку, просто текстовым файлом
    public DateTime CreationDate { get; set; }
    public DateTime? EditDate { get; set; }
    public bool IsCompleted { get; set; }
}
