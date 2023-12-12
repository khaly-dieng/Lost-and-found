using Application;
using Application.Abstractions.Repositories;
using Application.Behaviours;
using Infrastructure;
using Infrastructure.AutoMapper;
using Infrastructure.Data;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddDbContext<LostAndFoundDbContext>(options =>
{
    var ConnectionString = builder.Configuration.GetConnectionString("local");
    options.UseNpgsql(ConnectionString);
});

builder.Services.AddAutoMapper(typeof(AutoMapperConfigProfile));

//Add Extension in WebHostExtensions;RegisterRepositories;
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Pipelines
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(WatchBehavior<,>));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


