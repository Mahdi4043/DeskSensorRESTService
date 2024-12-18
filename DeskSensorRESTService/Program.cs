using DeskSensorRESTService.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 
builder.Services.AddControllers();

var optionsBuilder = new DbContextOptionsBuilder<DeskDbContext>();

//string? connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

optionsBuilder.UseSqlServer("Data Source=mssql12.unoeuro.com;Initial Catalog=auden_dk_db_eksamen;User ID=auden_dk;Password=5pFwR4c9bfEDGe3Bdymh;TrustServerCertificate = True");
DeskDbContext dbContext = new(optionsBuilder.Options);

builder.Services.AddSingleton<DeskRepoDb>(new DeskRepoDb(dbContext));
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
