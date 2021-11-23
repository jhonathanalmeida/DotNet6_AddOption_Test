using GrpcServiceWithRealishScenario.Configurations;
using GrpcServiceWithRealishScenario.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

builder.Services.AddOptions();

builder.Configuration.AddJsonFile("appsettings.json", false, true);

builder.Services.AddOptions<ServiceConfiguration>()
    .Bind(builder.Configuration.GetSection("GreeterServiceSettings:ServiceA"));
builder.Services.AddOptions<ServiceConfiguration>()
    .Bind(builder.Configuration.GetSection("GreeterServiceSettings:ServiceB"));

//builder.Services.Configure<ServiceConfiguration>("ServiceA",
//    builder.Configuration.GetSection("GreeterServiceSettings:ServiceA"));
//builder.Services.Configure<ServiceConfiguration>("ServiceB",
//    builder.Configuration.GetSection("GreeterServiceSettings:ServiceB"));

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterServiceA>();
app.MapGrpcService<GreeterServiceB>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();