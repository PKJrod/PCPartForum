using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPartForum.Models
{
    public class BlobStorageHelper
    {
        private IConfiguration _config;

        public BlobStorageHelper(IConfiguration config)
        {
            _config = config;
        }
        
        public async Task<string>
    }
}
