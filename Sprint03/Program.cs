using Microsoft.EntityFrameworkCore;
using Sprint03.adapter.output.database; // Certifique-se de que este namespace esteja correto e tenha a classe ApplicationDbContext
using Sprint03.adapter.input;
using Sprint03.domain.useCase;
using Sprint03.domain.repository;
using FluentValidation;
using Sprint03.adapter.input.dto;
using Sprint03.domain.model;
using Sprint03.domain.useCase.dto;
using Sprint03.infra.validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load environment variables from .env file
DotNetEnv.Env.Load();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseOracle(
        "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521)))\n(CONNECT_DATA=(SERVER=DEDICATED)(SID=ORCL)));User Id=RM97707;Password=220600;");
});

// Adiciona controladores
builder.Services.AddControllers();

// Registra adaptadores, casos de uso e repositórios
builder.Services.AddScoped<ICustomerAdapter, CustomerAdapter>();
builder.Services.AddScoped<ICustomerUseCase, CustomerUseCase>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Registra validadores
builder.Services.AddScoped<IValidator<Customer>, CustomerValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // Garante que os endpoints dos controladores sejam mapeados

app.MapGet("/weatherforecast", () =>
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
