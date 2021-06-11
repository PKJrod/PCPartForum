using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PCPartForum.Models
{
    public class BlobStorageHelper : IStorageHelper
    {
        private IConfiguration _config;

        public BlobStorageHelper(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Method to use blob storage for profile pictures to save on resources
        /// https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet
        /// </summary>
        /// <param name="profilepicture"></param>
        /// <returns></returns>
        public async Task<string> UploadBlob(IFormFile profilepicture)
        {
            string connect = _config.GetSection("BlobStorageString").Value;

            BlobServiceClient blobService = new BlobServiceClient(connect);

            //Ensure Create container to hold blobs
            BlobContainerClient containerClient = blobService.GetBlobContainerClient("userprofilephoto");

            if (!containerClient.Exists())
            {
                await containerClient.CreateAsync();
                await containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
            }

            // Add Blob to container
            string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(profilepicture.FileName);
            BlobClient blobClient = containerClient.GetBlobClient(newFileName);

            await blobClient.UploadAsync(profilepicture.OpenReadStream());
            return newFileName;
        }
    }
}
