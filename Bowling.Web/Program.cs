using Bowling.Core.Interfaces.Repositories;
using Bowling.Core.Interfaces;
using Bowling.Infrastructure.Data;
using Bowling.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Bowling.Core.Interfaces.Services;
using Bowling.Services;

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


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
