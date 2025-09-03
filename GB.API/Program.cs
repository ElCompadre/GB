using AutoMapper;
using GB.Application;
using GB.Application.Profiles;
using GB.Infrastructure;
using Serilog;
using Serilog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGBContext(builder.Configuration);
builder.Services.AddServices();

builder.Services.AddControllers();
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