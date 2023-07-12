namespace MyTrainer.API.Models;

public class CreateTrainingDTO
{
    public Guid TrainerId { get; set; }     
    public Guid UserId { get; set; }           
    public string? Name { get; set; }      
    public string Description { get; set; } 
    public DateTime CreationDate { get; set; }
}

