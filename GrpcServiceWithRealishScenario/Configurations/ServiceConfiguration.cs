using GrpcServiceWithRealishScenario.Configurations.Interfaces;

namespace GrpcServiceWithRealishScenario.Configurations;

public class ServiceConfiguration : IIdentifiableConfiguration
{
    public string ConfigurationName { get; set; } = string.Empty;
    public string ServiceDisplayName { get; set; } = string.Empty;
}