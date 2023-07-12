using Microsoft.AspNetCore.Mvc;
using MyTrainer.API.Models;
using MyTrainer.Application;
using MyTrainer.Application.Extensions;
using MyTrainer.Domain;

namespace MyTrainer.API.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class TrainingsController : Controller
{
    readonly ITrainingRepository _repository;
    private readonly ILogger<TrainingsController> _logger;

    public TrainingsController(ITrainingRepository repository)
        => _repository = repository;


    [HttpGet]
    [Route("get_trainings")]
    public IEnumerable<GetTrainingDTO> GetTrainings()
    {
        var trainings = _repository.GetAllTrainings().ToArray();
        var mappedTrainings = new List<GetTrainingDTO>();
        foreach (var training in trainings)
        {
            
            mappedTrainings.Add(new GetTrainingDTO()
            {
                Id = training.Id,
                UserId = training.UserId,
                TrainerId = training.TrainerId,
                Name = training.Name,
                Description = training.Description,
                CreationDate = training.CreationDate.ToDateTime(new TimeOnly(0)),
                EditDate = training.EditDate?.ToDateTime(new TimeOnly(0)),
                IsCompleted = training.IsCompleted
            });
        }

        return mappedTrainings;
    }


    [HttpGet]
    [Route("get_training/{guid:guid?}")]
    public GetTrainingDTO? GetTraining(Guid? guid)
    {
        //FIXME: ОБЯЗАТЕЛЬНО ИСПРАВИТЬ, НЕОБХОДИМО ВЕРНУТЬ ОШИБКУ. Реализация ниже (предположительно) может быть улучшена
        if (guid == null)
        {
            Response.StatusCode = 404;
            return null;
        }

        var training = _repository.Get(guid.Value);

        if (training == null)
            return null;

        var mappedTraining = new GetTrainingDTO()
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

        return mappedTraining;
    }


    [HttpPost]
    [Route("create_training")]
    public GetTrainingDTO CreateTraining(CreateTrainingDTO command)
    {
        //TODO: Здесь необходимо реализовать валидацию ID тренера и юзера (возможно, но в рамках микросервиса это не нужно думаю)
        var training = new Training()
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            TrainerId = command.TrainerId,
            Name = command.Name,
            Description = command.Description,
            CreationDate = command.CreationDate.ToDateOnly(),
            EditDate = null,
            IsCompleted = false
        };

        //TODO: подумать о реализации проверки успешности создания тренировки
        _repository.Create(training);
        _repository.Save();

        var returnedTraining = new GetTrainingDTO()
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

        return returnedTraining;
    }


    [HttpDelete]
    [Route("delete_training/{guid:guid?}")]
    public void DeleteTraining(Guid? guid) 
    {
        //FIXME: ОБЯЗАТЕЛЬНО ИСПРАВИТЬ, НЕОБХОДИМО ВЕРНУТЬ ОШИБКУ. Реализация ниже (предположительно) может быть улучшена
        if (guid == null)
        {
            Response.StatusCode = 404;
            return;
        }

        _repository.Delete(guid.Value);
    }


    [HttpPut]
    [Route("update_training/{guid:guid?}")]
    public GetTrainingDTO? UpdateTraining(UpdateTrainingDTO command, Guid? guid)
    {
        //FIXME: ОБЯЗАТЕЛЬНО ИСПРАВИТЬ, НЕОБХОДИМО ВЕРНУТЬ ОШИБКУ. Реализация ниже (предположительно) может быть улучшена
        if (guid == null)
        {
            Response.StatusCode = 404;
            return null;
        }

        //TODO: Здесь необходимо реализовать валидацию ID тренера и юзера (возможно, но в рамках микросервиса это не нужно думаю)
        var training = new Training()
        {
            Id = guid.Value,
            UserId = command.UserId,
            TrainerId = command.TrainerId,
            Name = command.Name,
            Description = command.Description,
            CreationDate = command.CreationDate.ToDateOnly(),
            EditDate = command.EditDate?.ToDateOnly(),
            IsCompleted = command.IsCompleted
        };

        //TODO: подумать о реализации проверки успешности обновления тренировки
        _repository.Update(training);
        _repository.Save();

        var returnedTraining = new GetTrainingDTO()
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

        return returnedTraining;
    }


    [HttpGet]
    [Route("HeartBeatTest")]
    public string HeartBeatTest()
        => "Service is now enable";
}
