using Microsoft.EntityFrameworkCore;
using MyTrainer.Application.Exceptions;
using MyTrainer.Application.Interfaces;
using MyTrainer.Domain;

namespace MyTrainer.Persistence.EntityFramework;

public class TrainingDbContextEf: DbContext, ITrainingDbContext
{

    public DbSet<Training> Trainings { get; set; }
    private static readonly string _tableName = "trainings";

    public TrainingDbContextEf(DbContextOptions<TrainingDbContextEf> options) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //FIXME: сделал логгирование в консоль для тестов, потом убрать
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Training>().ToTable(_tableName);

        modelBuilder.Entity<Training>().Property(t => t.Id).HasColumnName("id");
        modelBuilder.Entity<Training>().Property(t => t.UserId).HasColumnName("user_id");
        modelBuilder.Entity<Training>().Property(t => t.TrainerId).HasColumnName("trainer_id");
        modelBuilder.Entity<Training>().Property(t => t.Name).HasColumnName("name");
        modelBuilder.Entity<Training>().Property(t => t.Description).HasColumnName("description");
        modelBuilder.Entity<Training>().Property(t => t.CreationDate).HasColumnName("creation_date");
        modelBuilder.Entity<Training>().Property(t => t.EditDate).HasColumnName("edit_date");
        modelBuilder.Entity<Training>().Property(t => t.IsCompleted).HasColumnName("is_completed");

    }

    public void Create(Training training)
    {
        Trainings.Add(training);
    }

    public void Delete(Guid guid)
    {
        var training = Trainings.Where(tr => tr.Id == guid).FirstOrDefault() ?? throw new EntityNotFoundException();
        Trainings.Remove(training);
    }

    public Training? Get(Guid guid)
    {
        return Trainings.Where(training => training.Id == guid).FirstOrDefault();
    }

    public IEnumerable<Training> GetAllTrainings()
    {
        return Trainings;
    }

    public void Update(Training training)
    {
        var updatedTraining =  Trainings.Where(tr => tr.Id == training.Id).FirstOrDefault() ?? throw new EntityNotFoundException();
        Trainings.Update(training);
    }

    public void SaveContext()
    {
        SaveChanges();
    }

}

