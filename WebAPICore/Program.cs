using WebAPICore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Configuration.AddJsonFile("appsettings.json", false, true);
//builder.Configuration.AddJsonFile($"appsettings.{environmentName.ToLowerInvariant()}.json", true, true);
//builder.Configuration.AddEnvironmentVariables();

//builder.Services.AddSingleton(builder.Configuration);

//builder.Services.Configure<MySettings>(builder.Configuration.GetSection("MySettings"));
builder.Services.AddOptions<MySettings>().Bind(builder.Configuration.GetSection("MySettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();