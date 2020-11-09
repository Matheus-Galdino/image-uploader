using System;
using System.IO;
using Azure.Storage.Blobs;
using System.Threading.Tasks;

namespace API.Models
{
    public class BlobStorage
    {
        private readonly string connectionString;
        private readonly BlobContainerClient containerClient;

        public BlobStorage()
        {
            this.connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTIONSTRING");
            this.containerClient = new BlobContainerClient(this.connectionString, Environment.GetEnvironmentVariable("CONTAINERNAME"));
        }

        public async Task UploadFile(Stream stream, string fileName)
        {
            BlobClient blobClient = this.containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(stream, true);
        }

        public async Task<Stream> GetFile(string name)
        {
            var blobClient = this.containerClient.GetBlobClient(name);
            var download = await blobClient.DownloadAsync();

            return download.Value.Content;
        }
    }
}
