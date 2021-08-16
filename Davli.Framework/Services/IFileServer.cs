using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Services
{
    public interface IFileServer
    {
        Task<Stream> GetObjectAsync(string pucketName, string fileName);
        Task UploadObjectAsync(string pucketName, string fileName, Stream stream);
    }
}
