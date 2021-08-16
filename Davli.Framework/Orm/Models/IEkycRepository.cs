using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Orm.Models
{
    public interface IDavliRepository
    {
        /// <summary>
        /// Commit tracked objects
        /// </summary>
        /// <returns></returns>
        Task<long> CommitAsync();
    }
}
