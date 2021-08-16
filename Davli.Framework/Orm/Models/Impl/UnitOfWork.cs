using Microsoft.EntityFrameworkCore;
using Davli.Framework.DI;
using Davli.Framework.Exceptions;
using Davli.Framework.Models;
using Davli.Framework.Orm.EntityFrameworkCode;
using Davli.Framework.Orm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Orm.Models.Impl
{
    public class UnitOfWork : IUnitOfWork, ITransientLifetime
    {
        private readonly DavliDBContext _db;
        private Dictionary<Type, object> _repositories;
        private readonly RequestContext _requestContext;
        public UnitOfWork(DavliDBContext dBContext, RequestContext requestContext)
        {
            _db = dBContext;
            _requestContext = requestContext;
            _repositories = new Dictionary<Type, object>();
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
        public IRepository<Tout> GenericRepository<Tout>()
        {
            if (_repositories.ContainsKey(typeof(Tout)))
                return (IRepository<Tout>)_repositories[typeof(Tout)];
            _db.RequestContext = _requestContext;
            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(Tout)), _db);
            _repositories.TryAdd(typeof(Tout), repositoryInstance);
            return (IRepository<Tout>)repositoryInstance;
        }
    }
}
