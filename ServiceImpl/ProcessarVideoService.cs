using Service;

namespace ServiceImpl
{
    public class ProcessarVideoService : IProcessarVideoService
    {
        public async Task<string> ProcessarVideo(string videoId)
        {
            return $"Sucesso com o {videoId}";
        }
    }
}
