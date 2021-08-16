using System;
using System.Collections.Generic;
using System.Text;

namespace Davli.Framework.Orm.Models
{
    /// <summary>
    /// This interface signed entities that are wanted to store creation information (who and when created).
    /// </summary>
    public interface ICreationConcept
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        DateTime CreationTime { get; set; }

        /// <summary>
        /// Id of the creator user of this entity.
        /// </summary>
        long? CreatorUserId { get; set; }
    }
}
