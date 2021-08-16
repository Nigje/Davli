using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Options
{
    public class JwtOption: IOptionModel
    {
        /// <summary>
        /// Secret key to generate jwt.
        /// </summary>
        public string SecretKey { get; set; }
    }
}
