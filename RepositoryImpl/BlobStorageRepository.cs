using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Repository;
using System.ComponentModel;

namespace RepositoryImpl
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly IConfiguration _configuration;

        public BlobStorageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendVideoAsync()
        {
            var connectionString = _configuration.GetConnectionString("");

            //_blobServiceClient.FindBlobsByTags(connectionString);

            var container = new BlobContainerClient(connectionString, "container-teste");
            var responseContainer = await container.CreateIfNotExistsAsync();

            if (responseContainer?.GetRawResponse().Status == 201)
                await container.SetAccessPolicyAsync(PublicAccessType.Blob);

            container.GetBlobClient("");

        }
    }
}
