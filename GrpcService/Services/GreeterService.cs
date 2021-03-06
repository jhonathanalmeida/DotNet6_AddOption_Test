using Grpc.Core;

using Microsoft.Extensions.Options;

namespace GrpcService.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly MySettings _mySettings;

    public GreeterService(ILogger<GreeterService> logger, IEnumerable<IOptions<MySettings>> mySettings)
    {
        _logger = logger;

        var allSettings = mySettings.ToList();

        if (allSettings.Count() > 1)
        {
            _logger.LogError($"Multiple injections detected, count: {allSettings.Count()}");
        }

        _mySettings = allSettings.Single().Value; //will throw exception, as there are 2 instances of MySettings
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"{_mySettings.ServerName} Called.");

        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name + " from " + _mySettings.ServerName
        });
    }
}