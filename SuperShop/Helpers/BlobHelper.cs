using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SuperShop.Helpers
{
    public class BlobHelper : IBlobHelper
    {
        private readonly CloudBlobClient _blobClient;

        public BlobHelper(IConfiguration configuration)
        {
            string keys = configuration["Blob:ConnectionString"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(keys);
            _blobClient = storageAccount.CreateCloudBlobClient();
        }
        public async Task<Guid> UploadBlobAsync(IFormFile file, string containerNamer)
        {
            Stream stream = file.OpenReadStream();
            return await UploadStreamAsync(stream, containerNamer);
        }

        public async Task<Guid> UploadBlobAsync(byte[] file, string containerNamer)
        {
            MemoryStream stream = new MemoryStream(file);
            return await UploadStreamAsync(stream, containerNamer);
        }

        public async Task<Guid> UploadBlobAsync(string image, string containerNamer)
        {
            Stream stream = File.OpenRead(image);
            return await UploadStreamAsync(stream, containerNamer);
        }

        private async Task<Guid> UploadStreamAsync(Stream stream, string containerNamer)
        {
            Guid name = Guid.NewGuid();
            CloudBlobContainer container = _blobClient.GetContainerReference(containerNamer);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{name}");
            await blockBlob.UploadFromStreamAsync(stream);
            return name;
        }
    }
}
