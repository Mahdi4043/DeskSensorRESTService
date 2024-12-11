using DeskSensorRESTService.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 
builder.Services.AddControllers();

var optionsBuilder = new DbContextOptionsBuilder<DeskDbContext>();

string? connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

optionsBuilder.UseSqlServer(connectionString);
DeskDbContext dbContext = new(optionsBuilder.Options);

builder.Services.AddSingleton<DeskRepoDb>(new DeskRepoDb(dbContext));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                              });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
