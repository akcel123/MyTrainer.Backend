using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrainer.Application.Trainings.Commands;

public class UpdateTrainingCommand
{
    public Guid UserId { get; set; }        
    public Guid TrainerId { get; set; }     
    public string? Name { get; set; }       
    public string Description { get; set; } 
    public DateTime CreationDate { get; set; }
    public DateTime? EditDate { get; set; }
    public bool IsCompleted { get; set; }
}
