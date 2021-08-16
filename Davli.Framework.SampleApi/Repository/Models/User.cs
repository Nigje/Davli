using Davli.Framework.Orm.Models.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Davli.Framework.SampleApi.Repository.Models
{
    public class User : DavliEntity<long>
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
