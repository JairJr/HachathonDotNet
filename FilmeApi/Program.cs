using Microsoft.AspNetCore.Mvc;
using Service;
using ServiceImpl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProcessarVideoService, ProcessarVideoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/filme/enviar", async ([FromBody] string name) =>
{
    
    var service = app.Services.GetService<IProcessarVideoService>();

    var result = await service.ProcessarVideo(name);

    return Results.Ok(result);
});

app.Run();
