using Minio;
using Davli.Framework.DI;
using Davli.Framework.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Services.Impl
{
    public class FileService : IFileService, ITransientLifetime
    {
        private readonly IFileServer _fileServer;

        public FileService(IFileServer fileServer)
        {
            _fileServer = fileServer;
        }

        public async Task<Stream> GetFiletAsync(string pucketName, string fileName)
        {
            var stream= await _fileServer.GetObjectAsync(pucketName, fileName);
            return stream;
        }

        public async Task UploadFileAsync(string pucketName, string fileName, Stream stream)
        {
            await _fileServer.UploadObjectAsync( pucketName,  fileName,  stream);
        }
    }
}
