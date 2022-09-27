using SleekFlow.ToDo.WebApi.DataAccess;
using SleekFlow.ToDo.WebApi.DataAccess.Implementations;
using SleekFlow.ToDo.WebApi.MappingProfiles;
using SleekFlow.ToDo.WebApi.Services;
using SleekFlow.ToDo.WebApi.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();

builder.Services.AddAutoMapper(x => x.AddProfile(typeof(ToDoMappingProfile)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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