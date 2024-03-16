using Entities.DTO;
using Repository;
using Service;
using System.Net;

namespace ServiceImpl
{
    public class ProcessarVideoService : IProcessarVideoService
    {

        private readonly ISendToServiceBusRepository _repository;

        public ProcessarVideoService(ISendToServiceBusRepository repository)
        {
            _repository = repository;
        }

        public async Task<HttpStatusCode> ProcessarVideo(List<Tuple<string, FileStream>> videos)
        {
            try
            {
                foreach (var video in videos)
                {

                    var enviarVideoRequest = new EnviarVideoRequest(video.Item1, video.Item2);

                    //Criar Logica para quando der erro na tentativa de salvar mensagem na fila

                    await _repository.EnviarVideoAsync(enviarVideoRequest);
                }

                return HttpStatusCode.OK;

            }
            catch
            {

//                return HttpStatusCode.;
            }
        }
    }
}
