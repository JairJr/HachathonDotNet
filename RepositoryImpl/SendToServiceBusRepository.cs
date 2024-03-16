using Entities.DTO;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Repository;

namespace RepositoryImpl
{
    public class SendToServiceBusRepository : ISendToServiceBusRepository
    {

        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public SendToServiceBusRepository(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        public async Task EnviarVideoAsync(EnviarVideoRequest request)
        {
            var nomeFila = _configuration["MassTransitAzure:NomeFila"] ?? string.Empty;

            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));

            await endpoint.Send(request);

        }
    }
}
