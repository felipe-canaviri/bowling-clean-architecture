using Bowling.Core.Interfaces.Repositories;
using Bowling.Core.Interfaces;
using Bowling.Infrastructure.Data;
using Bowling.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Bowling.Core.Interfaces.Services;
using Bowling.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("BowlingDB"),
                    x => x.MigrationsAssembly("Bowling.Infrastructure"))
            );

builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IGameRepository), typeof(GameRepository));
builder.Services.AddScoped(typeof(IPlayerRepository), typeof(PlayerRepository));
builder.Services.AddScoped(typeof(ITurnRepository), typeof(TurnRepository));

builder.Services.AddScoped(typeof(IGameService), typeof(GameService));
builder.Services.AddScoped(typeof(IPlayerService), typeof(PlayerService));
builder.Services.AddScoped(typeof(ITurnService), typeof(TurnService));
builder.Services.AddScoped(typeof(IScoreService), typeof(ScoreService));


builder.Services.AddControllers();
builder.Services.AddResponseCaching();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Bowling WebAPI",
        Description = "Simple Sample WebAPI project to simulate a Bowling game app."
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c => { 
        c.SerializeAsV2 = true;
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseResponseCaching();

app.Run();
