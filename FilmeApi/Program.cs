using MassTransit;
using Repository;
using RepositoryImpl;
using Service;
using ServiceImpl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProcessarVideoService, ProcessarVideoService>();
builder.Services.AddTransient<IServiceBusRepository, ServiceBusRepository>();
builder.Services.AddTransient<IBlobStorageRepository, BlobStorageRepository>();

builder.Services.AddMassTransit(configuracoes =>
{
    configuracoes.UsingAzureServiceBus((contexto, configuracoesServiceBus) =>
    {
        configuracoesServiceBus.Host(builder.Configuration["MassTransitAzure:Conexao"]);
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

app.MapPost("/filme/enviar", async (IFormFileCollection videos) =>
{

    var service = app.Services.GetService<IProcessarVideoService>();

    List<Tuple<string, FileStream>> videosStream = await ConvertToFileStream(videos);

    await service.ProcessarVideo(videosStream);

    return Results.Ok();

}).Accepts<IFormFile>("multipart/form-data").DisableAntiforgery();

app.Run();

static async Task<List<Tuple<string, FileStream>>> ConvertToFileStream(IFormFileCollection videos)
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