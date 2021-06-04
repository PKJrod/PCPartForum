using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PCPartForum.Models
{
    public interface IStorageHelper
    {
        Task<string> UploadBlob(IFormFile profilepicture);
    }
}