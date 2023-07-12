namespace MyTrainer.Domain;

public class Training
{
    public Guid UserId { get; set; }        // ID пользователя, которому предназначена тренировка
    public Guid TrainerId { get; set; }     // Вот это свойство под вопросом (ID тренера, который написал тренировку)
    public Guid Id { get; set; }            // ID самой тренировки
    public string? Name { get; set; }       // Название тренировки (например, "силовая тренировка ног")
    public string Description { get; set; } // Определяет всю тренировку, просто текстовым файлом
    public DateOnly CreationDate { get; set; }
    public DateOnly? EditDate { get; set; }
    public bool IsCompleted { get; set; }    
}
