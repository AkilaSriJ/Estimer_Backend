using AutoMapper;
using GenXThofa.Technologies.Estimer.API.DependencyInjection;
using GenXThofa.Technologies.Estimer.BusinessLogic.Mapper;
using GenXThofa.Technologies.Estimer.Data;
using GenXThofa.Technologies.Estimer.Data.Context;
using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options=>options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),new MySqlServerVersion(new Version(9,1,0))));
builder.Services.AddAutoMapper(typeof(ProjectStatusProfile).Assembly);
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddAutoMapper(typeof(RoleProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ClientProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ProjectProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ProjectTeamProfile).Assembly);
builder.Services.AddAutoMapper(typeof(MileStoneStatusProfile).Assembly);
builder.Services.AddAutoMapper(typeof(MileStoneProfile).Assembly);
builder.Services.AddAutoMapper(typeof(EmployeeProfile).Assembly);
builder.Services.AddHttpClient<IEmployeeRepository, EmployeeRepository>(client =>
{
    var jsonServerUrl = builder.Configuration["JsonServer:BaseUrl"]
        ?? "http://localhost:3000/";

    client.BaseAddress = new Uri(jsonServerUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromSeconds(30);
})
.AddPolicyHandler(GetRetryPolicy())
.AddPolicyHandler(GetCircuitBreakerPolicy());



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataDI();
builder.Services.AddServiceDI();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
var app = builder.Build();
app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var jsonServerUrl = builder.Configuration["JsonServer:BaseUrl"] ?? "http://localhost:3000/";
Console.WriteLine($"JSON Server URL configured: {jsonServerUrl}");

app.Run();
static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(
            retryCount: 3,
            sleepDurationProvider: retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
        );
}

static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(
            handledEventsAllowedBeforeBreaking: 2,
            durationOfBreak: TimeSpan.FromSeconds(30)
        );
}
