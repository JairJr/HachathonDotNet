using Entities.DTO;

namespace Repository
{
    public interface IBlobStorageRepository
    {
        public Task SendVideoAsync(EnviarVideoRequest enviarVideoRequest, FileStream arquivo);
    }
}
