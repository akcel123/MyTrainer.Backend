using Microsoft.AspNetCore.Mvc;
using MyTrainer.API.Models;
using MyTrainer.Application;
using System.Text.Json;

namespace MyTrainer.API.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class TrainingsController : Controller
{
    readonly ITrainingRepository _repository;
    private readonly ILogger<TrainingsController> _logger;
    
    public TrainingsController(ITrainingRepository repository, ILogger<TrainingsController> logger)
        => (_repository, _logger) = (repository, logger);

    [HttpGet]
    [Route("get_trainings")]
    public IEnumerable<GetTrainingDto> GetTrainings()
    {

        var trainings = _repository.GetAllTrainings().ToArray();
        var mappedTrainings = new List<GetTrainingDto>();

        foreach (var training in trainings)
            mappedTrainings.Add(GetTrainingDto.FromTraining(training));

        _logger.LogInformation("Все тренировки успешно получены в ответ на запрос");

        return mappedTrainings;
    }


    [HttpGet]
    [Route("get_training/{guid:guid}")]
    public GetTrainingDto? GetTraining(Guid guid)
    {
        var training = _repository.Get(guid);

        if (training == null)
        {
            _logger.LogError("Не удалось получить тренировку из БД");
            return null;
        }

        var mappedTraining = GetTrainingDto.FromTraining(training);

        return mappedTraining;
    }


    [HttpPost]
    [Route("create_training")]
    public GetTrainingDto CreateTraining(CreateTrainingDto dto)
    {
        _logger.LogInformation("Создание тренировки. DTO: " + JsonSerializer.Serialize(dto));

        //TODO: Здесь необходимо реализовать валидацию ID тренера и юзера (возможно, но в рамках микросервиса это не нужно думаю)
        var training = dto.ToTraining();

        _repository.Create(training);

        var returnedTraining = GetTrainingDto.FromTraining(training);

        return returnedTraining;
    }


    [HttpDelete]
    [Route("delete_training/{guid:guid}")]
    public void DeleteTraining(Guid guid) 
    {
        _repository.Delete(guid);
    }


    [HttpPut]
    [Route("update_training/{guid:guid}")]
    public GetTrainingDto? UpdateTraining(UpdateTrainingDTO dto, Guid guid)
    {
        _logger.LogInformation("Обновление события. DTO: " + JsonSerializer.Serialize(dto));

        //TODO: Здесь необходимо реализовать валидацию ID тренера и юзера (возможно, но в рамках микросервиса это не нужно думаю)
        var training = dto.ToTraining(guid);

        _repository.Update(training);

        var returnedTraining = GetTrainingDto.FromTraining(training);
      
        return returnedTraining;
    }


    [HttpGet]
    [Route("HeartBeatTest")]
    public string HeartBeatTest()
        => "Service is now enable";
}
