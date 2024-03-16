using Entities.DTO;

namespace Repository
{
    public interface ISendToServiceBusRepository
    {

        public Task EnviarVideoAsync(EnviarVideoRequest request);

    }
}
