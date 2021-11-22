using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebAPICore.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly MySettings _mySettings;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
        IEnumerable<IOptions<MySettings>> mySettings)
    {
        _logger = logger;

        var allSettings = mySettings.ToList();

        if (allSettings.Count() > 1)
        {
            _logger.LogError($"Multiple injections detected, count: {allSettings.Count()}");
        }

        _mySettings = allSettings.Single().Value; //will throw exception, as there are 2 instances of MySettings
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation($"{_mySettings.ServerName} called");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)] + " from " + _mySettings.ServerName
            })
            .ToArray();
    }
}