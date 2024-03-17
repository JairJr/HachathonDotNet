using Azure.Storage.Blobs;
using Entities.DTO;
using Microsoft.Extensions.Configuration;
using Repository;

namespace RepositoryImpl
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly IConfiguration _configuration;

        public BlobStorageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendVideoAsync(EnviarVideoRequest enviarVideoRequest, FileStream arquivo)
        {

            // Criar um cliente BlobServiceClient
            var connectionString = _configuration.GetConnectionString("AzureBlobStorage");
            var blobServiceClient = new BlobServiceClient(connectionString);

            // Obter um contêiner de blob
            var containerClient = blobServiceClient.GetBlobContainerClient("videos");
            await containerClient.CreateIfNotExistsAsync();

            // Criar um BlobClient para o blob
            var blobClient = containerClient.GetBlobClient(enviarVideoRequest.NomeVideo);

            // Carregar um arquivo para o blob
            await blobClient.UploadAsync(arquivo);

        }
    }
}
