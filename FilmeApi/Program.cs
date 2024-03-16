using MassTransit;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Repository;
using RepositoryImpl;
using Service;
using ServiceImpl;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProcessarVideoService, ProcessarVideoService>();
builder.Services.AddTransient<ISendToServiceBusRepository, SendToServiceBusRepository>();

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = long.MaxValue;
    options.MultipartBoundaryLengthLimit = int.MaxValue;
    options.ValueCountLimit = int.MaxValue;
    options.BufferBodyLengthLimit = long.MaxValue;
    options.MultipartHeadersCountLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});

builder.Services.AddMassTransit(configuracoes =>
{
    configuracoes.UsingAzureServiceBus((contexto, configuracoesServiceBus) =>
    {
        configuracoesServiceBus.Host("Endpoint=sb://sb-hackathonfiap.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ebp1GU+MvKL5LC5F63VZgv4qLituxrqkq+ASbKyaSao=");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/filme/enviar", async ([FromForm] IFormFile[] videos) =>
{

    var service = app.Services.GetService<IProcessarVideoService>();

    List<Tuple<string, FileStream>> videosStream = await ConvertToFileStream(videos);

    await service.ProcessarVideo(videosStream);

    return Results.Ok();

}).Accepts<IFormFile>("multipart/form-data").DisableAntiforgery();

app.Run();

static async Task<List<Tuple<string, FileStream>>> ConvertToFileStream(IFormFile[] videos)
{
    var videosStream = new List<Tuple<string, FileStream>>();

    foreach (var video in videos)
    {
        using var fileStream = new FileStream(Path.GetTempFileName(), FileMode.Create);

        await video.CopyToAsync(fileStream);

        fileStream.Seek(0, SeekOrigin.Begin);

        videosStream.Add(new Tuple<string, FileStream>(video.FileName, fileStream));
    }

    return videosStream;
}