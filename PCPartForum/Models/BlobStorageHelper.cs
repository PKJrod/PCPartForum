using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
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
        /// if running to issues with azure
        /// https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows
        /// </summary>
        /// <param name="profilepicture"></param>
        /// <returns></returns>
        public async Task<string> UploadBlobToAzure(IFormFile profilepicture)
        {
            string connect = _config.GetSection("ConnectionStrings:BlobStorageString").Value;
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connect);

            //Ensure Create container to hold blobs
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var containerClient = cloudBlobClient.GetContainerReference("profilepic");

            if (await containerClient.CreateIfNotExistsAsync())
            {
                await containerClient.SetPermissionsAsync(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Off });
            }

            // Add Blob to container
            string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(profilepicture.FileName);
            var blobClient = containerClient.GetBlockBlobReference(newFileName);
            blobClient.Properties.ContentType = profilepicture.ContentType;

            await blobClient.UploadFromStreamAsync(profilepicture.OpenReadStream());
            return newFileName;
        }
    }
}
