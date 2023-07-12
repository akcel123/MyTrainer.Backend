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
}
