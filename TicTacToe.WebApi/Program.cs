using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.GameLogic;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Services;
using TicTacToe.Infrastructure;
using TicTacToe.WebApi.Hubs;

Console.WriteLine("TicTacToe WebApi - START");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFastEndpoints();
builder.Services.AddSignalR();

builder.Services.SwaggerDocument(opt =>
{
    opt.MaxEndpointVersion = 1;
    opt.DocumentSettings = s =>
    {
        s.Title = $"TicTacToe WebApi";
        s.Version = "v1";
    };
    opt.ShortSchemaNames = true;
});

var commandTimeout = builder.Configuration.GetValue<int?>("DatabaseSettings:CommandTimeout");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Database"),
        // Время ожидания было увеличено, т.к. стандартных 30 секунд не всегда хватает для запросов/миграций.
        npgsqlOpt => npgsqlOpt.CommandTimeout(commandTimeout ?? 120)));

builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<IGameEngine, GameEngine>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddTransient<IRoomCodeGenerator, RoomCodeGenerator>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.MapHub<GameHub>("/hubs/game");

app.UseFastEndpoints(c =>
{
    c.Versioning.Prefix = "v";
    c.Versioning.DefaultVersion = 1;
    c.Versioning.PrependToRoute = true;
    c.Endpoints.RoutePrefix = "api";
    c.Endpoints.ShortNames = true;
    c.Endpoints.Configurator = config => config.AllowAnonymous();
});
app.UseSwaggerGen();

app.UseCors();

//using var scope = app.Services.CreateScope();
//using (var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
//{
//    dbContext.Database.Migrate();
//}

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.Run();
