using Grpc.Core;

using GrpcServiceWithRealishScenario.Configurations;

using Microsoft.Extensions.Options;

namespace GrpcServiceWithRealishScenario.Services;

public class GreeterServiceA : GreeterA.GreeterABase
{
    private const string ServiceConfigKey = "ServiceAKey";
    private readonly IOptionsSnapshot<ServiceConfiguration> _namedOptionsAccessor;

    private readonly ServiceConfiguration _serviceConfiguration;

    public GreeterServiceA(IEnumerable<IOptions<ServiceConfiguration>> allServiceConfigurations,
        IOptionsSnapshot<ServiceConfiguration> namedOptionsAccessor)
    {
        _namedOptionsAccessor = namedOptionsAccessor; // if we use this, the all works fine

        _serviceConfiguration = allServiceConfigurations.Select(sc => sc.Value)
            .Single(sc => sc.ConfigurationName == ServiceConfigKey);
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = $"Hello {request.Name} from {_serviceConfiguration.ServiceDisplayName}"
        });
    }
}