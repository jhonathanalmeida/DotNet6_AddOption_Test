using Grpc.Core;

using GrpcServiceWithRealishScenario.Configurations;

using Microsoft.Extensions.Options;

namespace GrpcServiceWithRealishScenario.Services;

public class GreeterServiceB : GreeterB.GreeterBBase
{
    private const string ServiceConfigKey = "ServiceBKey";

    private readonly ServiceConfiguration _serviceConfiguration;

    public GreeterServiceB(
        IEnumerable<IOptions<ServiceConfiguration>> allServiceConfigurations)
    {
        _serviceConfiguration = allServiceConfigurations.Select(sc => sc.Value)
            .Single(sc => sc.ConfigurationName == ServiceConfigKey);
    }

    public override Task<HelloReply> SayHelloB(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = $"Hello {request.Name} from {_serviceConfiguration.ServiceDisplayName}"
        });
    }
}