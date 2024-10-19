using CursoIdiomas.Model.Context;
using CursoIdiomas.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//injetando repositories
builder.Services.AddScoped<ITurmaRepository,TurmaRepository>();
builder.Services.AddScoped<IAlunoRepository,AlunoRepository>();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MysqlContext>(options => options.UseMySql(connection,new MySqlServerVersion(new Version(8,0,5))));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();


app.Run();

