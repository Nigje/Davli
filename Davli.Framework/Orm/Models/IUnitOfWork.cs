using Davli.Framework.Orm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Orm
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        IRepository<Tout> GenericRepository<Tout>();
    }
}
