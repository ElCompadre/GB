using AutoMapper;
using GB.Application.Profiles;
using GB.Infrastructure;
using Serilog;
using Serilog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGBContext(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSerilog(config => 
    config.ReadFrom.Configuration(builder.Configuration)
    );

var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationProfile>(), new SerilogLoggerFactory());

mapperConfig.AssertConfigurationIsValid();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ApplicationProfile>());

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