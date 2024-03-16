using MassTransit;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using ServiceImpl;
using WorkerFilme;
using WorkerFilme.Eventos;


IHost builder = Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
{
    var configuration = hostContext.Configuration;

    #region MassTransit Azure Service Bus Settings
    var connectionString = configuration.GetSection("MassTransitAzure")["Conexao"] ?? string.Empty;

    services.AddMassTransit(x =>
    {
        x.AddConsumer<GeraImagens>();
        x.UsingAzureServiceBus((context, cfg) =>
        {
            cfg.Host(connectionString);
            cfg.ReceiveEndpoint(configuration.GetSection("MassTransitAzure")["NomeFila"], e =>
            {
                e.ConfigureConsumer<GeraImagens>(context);
            });
        });
    });
    #endregion

    //Adding DBContexts - Trocar nome da string de conexão
    string connectionStringSQLServer = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
    services.AddDbContext<VideoContext>(settings => settings.UseSqlServer(connectionStringSQLServer));        
    services.AddTransient<IWorkerVideoService, WorkerVideoService>();
    services.AddTransient<IVideoDbRepository, VideoDbRepository>();

    services.AddHostedService<Worker>();
})

    .Build();

builder.Run();
