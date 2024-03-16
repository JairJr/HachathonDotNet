using Entities.Comum;
using MassTransit;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerFilme.Eventos
{
    public class GeraImagens : IConsumer<RequestModel>
    {
        private readonly IWorkerVideoService _workerVideoService;

        public GeraImagens(IWorkerVideoService workerVideoService)
        {
                _workerVideoService = workerVideoService;
        }

        public async Task Consume(ConsumeContext<RequestModel> context)
        {
            var request = context.Message;
            var result = _workerVideoService.ExtraiImagens(request.Stream, request.NomeVideo);
            if (result)
            {
                Console.WriteLine("Imagens geradas com sucesso");
            }
            else
            {
                Console.WriteLine("Erro ao gerar imagens");
            }
        }

    }
}
