namespace ResumeAnalyzer.Api.Services
{
    using Azure.Storage.Blobs;
    using Microsoft.Extensions.Configuration;
    using ResumeAnalyzer.Api.Interfaces;

    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobContainerClient _container;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"];

            var containerName = configuration["ContainerName"];

            _container = new BlobContainerClient(
                connectionString,
                containerName);

            _container.CreateIfNotExists();
        }

        public async Task<string> UploadAsync(string fileName, Stream fileStream)
        {
            var blobName = $"{Guid.NewGuid()}-{fileName}";

            var blobClient = _container.GetBlobClient(blobName);

            await blobClient.UploadAsync(fileStream, overwrite: true);

            return blobName;
        }

        public async Task<Stream> DownloadAsync(string blobName)
        {
            var blobClient = _container.GetBlobClient(blobName);

            var response = await blobClient.DownloadStreamingAsync();

            return response.Value.Content;
        }
    }
}
