using Minio;
using Davli.Framework.DI;
using Davli.Framework.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Services.Impl
{
    public class MinioFileServer : IFileServer, ITransientLifetime
    {
        //********************************************************************************************************************************
        private readonly MinioClient _minioClient;
        private readonly MinioOption _minioOption;
        //********************************************************************************************************************************
        public MinioFileServer()
        {
            _minioOption = OptionService.GetOption<MinioOption>();
            if (_minioOption.UseSSL)
            {
                _minioClient = new MinioClient(_minioOption.Url, _minioOption.AccessKey, _minioOption.SecretKey).WithSSL();
            }
            else
            {
                _minioClient = new MinioClient(_minioOption.Url, _minioOption.AccessKey, _minioOption.SecretKey);
            }
        }
        //********************************************************************************************************************************
        public async Task UploadObjectAsync(string bucketName, string fileName, Stream stream)
        {
            await CreateBucketIfNotExistAsync(bucketName);
            await _minioClient.PutObjectAsync(bucketName, fileName, stream, stream.Length, null, null, _minioOption.ServerSideEncryption, default);
        }
        //********************************************************************************************************************************
        public async Task<Stream> GetObjectAsync(string bucketName, string fileName)
        {
            Stream stream=new MemoryStream();
            await _minioClient.GetObjectAsync(bucketName, fileName, str => str.CopyTo(stream), _minioOption.ServerSideEncryption);
            return stream;
        }
        //********************************************************************************************************************************
        private async Task CreateBucketIfNotExistAsync(string bucketName)
        {
            bool found = await _minioClient.BucketExistsAsync(bucketName);
            if (!found)
                await _minioClient.MakeBucketAsync(bucketName);
        }
        //********************************************************************************************************************************
    }
}
