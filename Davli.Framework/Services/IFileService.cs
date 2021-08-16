using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Services
{
    public interface IFileService
    {
        Task<Stream> GetFiletAsync(string pucketName, string fileName);
        Task UploadFileAsync(string pucketName, string fileName, Stream stream);
    }
}
