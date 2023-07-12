using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrainer.Application.Trainings.DTO;

public class CreateTrainingDTO
{
    public Guid TrainerId { get; set; }     // Вот это свойство под вопросом (ID тренера, который написал тренировку)
    public Guid UserId { get; set; }            // ID самой тренировки
    public string? Name { get; set; }       // Название тренировки (например, "силовая тренировка ног")
    public string Description { get; set; } // Определяет всю тренировку, просто текстовым файлом
    public DateTime CreationDate { get; set; }
}
