using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace API.Models
{
    public class BlobStorage
    {
        private string connectionString;
        private readonly BlobContainerClient containerClient;

        public BlobStorage()
        {            
            this.connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTIONSTRING");
            this.containerClient = new BlobContainerClient(connectionString, Environment.GetEnvironmentVariable("CONTAINERNAME"));
        }

        public async Task UploadFile(Stream stream, string fileName)
        {
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(stream, true);
        }

        public async Task<List<BlobItem>> GetAllFiles()
        {
            List<BlobItem> blobs = new List<BlobItem>();            
            await foreach (var item in containerClient.GetBlobsAsync())            
                blobs.Add(item);            

            return blobs;
        }
    }
}
