using System.Net;

namespace Service
{
    public interface IProcessarVideoService
    {
        public Task<HttpStatusCode> ProcessarVideo(List<Tuple<string, FileStream>> videos);
    }
}
