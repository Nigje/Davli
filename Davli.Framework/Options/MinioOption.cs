using Minio.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Options
{
    public class MinioOption : IOptionModel
    {
        public string Url { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public ServerSideEncryption ServerSideEncryption { get; set; }
        public bool UseSSL { get; set; }
    }
}
