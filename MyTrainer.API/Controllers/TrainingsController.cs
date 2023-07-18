using Microsoft.AspNetCore.Mvc;
using MyTrainer.API.Models;
using MyTrainer.Application;
using MyTrainer.Application.Exceptions;

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
    [Route("get_training/{guid:guid?}")]
    public GetTrainingDto? GetTraining(Guid? guid)
    {
        if (guid == null)
            throw new GuidIsNullException("При попытке получения тренировки, не передано значение id");

        var training = _repository.Get(guid.Value);

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
        _logger.LogInformation("Создание тренировки. DTO: " + dto);

        //TODO: Здесь необходимо реализовать валидацию ID тренера и юзера (возможно, но в рамках микросервиса это не нужно думаю)
        var training = dto.ToTrainig();

        _repository.Create(training);
        _repository.Save();

        var returnedTraining = GetTrainingDto.FromTraining(training);

        return returnedTraining;
    }


    [HttpDelete]
    [Route("delete_training/{guid:guid?}")]
    public void DeleteTraining(Guid? guid) 
    {
        if (guid == null)
            throw new GuidIsNullException("При попытке удаления тренировки, не передано значение id");

        _repository.Delete(guid.Value);
    }


    [HttpPut]
    [Route("update_training/{guid:guid?}")]
    public GetTrainingDto? UpdateTraining(UpdateTrainingDTO dto, Guid? guid)
    {
        if (guid == null)
            throw new GuidIsNullException("При попытке обновления тренировки, не передано значение id");

        _logger.LogInformation("Обновление события. DTO: " + dto);

        //TODO: Здесь необходимо реализовать валидацию ID тренера и юзера (возможно, но в рамках микросервиса это не нужно думаю)
        var training = dto.ToTraining(guid.Value);

        _repository.Update(training);
        _repository.Save();

        var returnedTraining = GetTrainingDto.FromTraining(training);
      
        return returnedTraining;
    }


    [HttpGet]
    [Route("HeartBeatTest")]
    public string HeartBeatTest()
        => "Service is now enable";
}
