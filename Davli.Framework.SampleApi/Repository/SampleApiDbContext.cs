using Davli.Framework.Models;
using Davli.Framework.Orm.EntityFrameworkCode;
using Davli.Framework.SampleApi.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Davli.Framework.SampleApi.Repository
{
    public class SampleApiDbContext: DavliDBContext
    {
        public SampleApiDbContext(DbContextOptions<SampleApiDbContext> options, RequestContext requestContext) : base(options, requestContext)
        {
            RequestContext = requestContext;
        }

        public DbSet<User> Users { get; set; }
    
    }
}
