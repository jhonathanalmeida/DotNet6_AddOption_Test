using GrpcService;
using GrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

builder.Configuration.AddJsonFile("appsettings.json", false, true);
//builder.Configuration.AddJsonFile($"appsettings.{environmentName.ToLowerInvariant()}.json", true, true);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddSingleton(builder.Configuration);

builder.Services.Configure<MySettings>(
    builder.Configuration.GetSection("MySettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();