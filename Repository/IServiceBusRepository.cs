using Entities.DTO;

namespace Repository
{
    public interface IServiceBusRepository
    {
        public Task SendVideoInfoToQueueAsync(EnviarVideoRequest request);
    }
}