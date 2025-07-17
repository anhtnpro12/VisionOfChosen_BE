using Microsoft.EntityFrameworkCore;
using VisionOfChosen_BE;
using VisionOfChosen_BE.Infra.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
{
    builder
       .AllowAnyMethod()
       .AllowAnyHeader()
                .SetIsOriginAllowed((host) => true) 
                .AllowCredentials();
}));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<VisionOfChosen_Context>(options =>
    options.UseSqlite("Data Source=app.db"));
builder.Services.AddServices();
var app = builder.Build();

app.UseCors("AllowOrigin");

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
